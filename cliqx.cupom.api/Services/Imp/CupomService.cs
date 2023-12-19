using cliqx.cupom.api.Controllers;
using cliqx.cupom.api.Models.RecargaPlus.Response;
using cliqx.cupom.api.Models.Request;
using cliqx.cupom.api.Models.Response;
using cliqx.cupom.api.Repositories;
using cliqx.cupom.api.Repositories.Contracts;
using cliqx.cupom.api.Services;
using cliqx.cupom.api.Services.RecargaPlus.Imp;
using cliqx.cupom.api.Validadores;
using Microsoft.Extensions.Configuration;

namespace cliqx.cupom.api.services.Imp
{
    public class CupomService : ICupomService
    {
        private readonly ICupomRepository _cupomRepository;
        private readonly ICupomUsoPedidoRepository _cupomUsoPedidoRepository;
        private readonly ICupomLimiteCpfRepository _cupomLimiteCpfRepository;
        public ICalculoDescontoService _calculoDescontoService { get; }
        private readonly RecargaPlusApi _recargaPlusApi;

        public CupomService(
           ICupomRepository cupomRepository,
           ICupomLimiteCpfRepository cupomLimiteCpfRepository,
           ICupomUsoPedidoRepository cupomUsoPedidoRepository,
           ICalculoDescontoService calculoDescontoService,
           IConfiguration configuration)
        {
            _cupomRepository = cupomRepository;
            _cupomLimiteCpfRepository = cupomLimiteCpfRepository;
            _cupomUsoPedidoRepository = cupomUsoPedidoRepository;
            _calculoDescontoService = calculoDescontoService;
            _recargaPlusApi = new RecargaPlusApi(configuration, configuration.GetSection("RecargaPlus")["UrlBase"]);
        }

        

        public async Task<Cupom> GetByCodigo(string codigo)
        {
            try
            {
                if (string.IsNullOrEmpty(codigo))
                    return null;

                return await _cupomRepository.GetByCodigo(codigo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar o cupom por código: " + ex.Message.ToString());
                throw;
            }
        }

        public async Task<Cupom> InsereCupom(Cupom cupom)
        {
            try
            {
                CupomValidador.ValidaCupom(cupom);

                var cupomEncontrado =  await _cupomRepository.GetByCodigo(cupom.CodigoCupom);
                if (cupomEncontrado != null)
                    throw new Exception($"O cupom já existe com esse código {cupom.CodigoCupom}. Tente utilizar outro código");

                _cupomRepository.Add(cupom);
                var salvo = await _cupomRepository.SaveChangesAsync();
                if (!salvo)
                    return null;

                return cupom;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir o cupom: " + ex.Message.ToString());
                throw;
            }
           
        }

        public async Task<AplicaCupomResponse> AplicaCupom(CupomAplicacaoRequest cupomEntrada)
        {
            try
            {
                PedidoResponse pedidoResponse = null;
                decimal valorTotal = 0;
                var response = new AplicaCupomResponse();
                var cupomEncontrado = await _cupomRepository.GetByCodigo(cupomEntrada.CodigoCupom);
                if (cupomEncontrado == null)
                    throw new Exception($"O cupom informado não existe!");

                //Verifica data de validade cupom
                CupomValidador.VerificaDataValidadeCupom(cupomEncontrado);

                //Calcula o desconto pelo valor enviado ou pelo valor total do pedido
                if (cupomEntrada.ValorACalcular > 0)
                {
                    valorTotal = cupomEntrada.ValorACalcular;
                    response.ValorTotalComDesconto = _calculoDescontoService.CalculaValorDesconto(
                        cupomEncontrado.TipoDesconto,
                        cupomEntrada.ValorACalcular,
                        cupomEncontrado.ValorDesconto,
                        cupomEncontrado.PercentualDesconto);
                }
                else
                {
                    var pedido = await _recargaPlusApi.Client.BuscaPedidoPorId(cupomEntrada.IdPedido.ToString());

                    if (!pedido.IsSuccessStatusCode)
                        throw new Exception($"Houve um problema na busca do pedido: {cupomEntrada.IdPedido}");

                    pedidoResponse = pedido.Content;
                    valorTotal = pedido.Content.ValorTotal.Value;

                    if (valorTotal > 0)
                    {
                        response.ValorTotalComDesconto = _calculoDescontoService.CalculaValorDesconto(
                          cupomEncontrado.TipoDesconto,
                          valorTotal,
                          cupomEncontrado.ValorDesconto,
                          cupomEncontrado.PercentualDesconto);
                    }
                }

                //Busca o pedido com os respectivos dados no recarga
                if (cupomEncontrado.TipoCupom == TipoCupom.PRIMEIRA_COMPRA)
                {
                    //Verifica se tem algum pedido no recarga como entregue
                    var statusPedidoComoEntregue = 4;
                    var pedidosEncontrados = await _recargaPlusApi.Client.BuscaPedidosPorCpfEStatus(cupomEntrada.Cpf, statusPedidoComoEntregue);
                    if (pedidosEncontrados.IsSuccessStatusCode)
                    {
                        if (pedidosEncontrados.Content != null && pedidosEncontrados.Content.Count > 0)
                            throw new Exception("O cupom informado não será valido, visto que não é a primeira compra do cliente!");
                    }
                }

                //Verifica quantidade Limite do cupom
                var quantidadeLimite = _cupomLimiteCpfRepository.BuscaQuantidadeCuponsPorCpf(cupomEntrada.Cpf);
                var quantidadeCuponsUsados = _cupomUsoPedidoRepository.QuantidadeCuponsUsadosParaOCpf(cupomEntrada.Cpf);
                if (quantidadeLimite > 0)
                {
                    if (quantidadeCuponsUsados > quantidadeLimite)
                        throw new Exception($"Você já usou {quantidadeCuponsUsados} cupons, portanto já ultrapassou o limite de {quantidadeLimite} estabelecido para o cpf {cupomEntrada.Cpf}!");
                }


                response.ValorDescontoCalculado = valorTotal - response.ValorTotalComDesconto;

                await AdicionaCupomUsoPedido(cupomEntrada, valorTotal, response, cupomEncontrado);

                response.Cpf = cupomEntrada.Cpf;
                response.CodigoCupom = cupomEntrada.CodigoCupom;
                response.IdPedido = cupomEntrada.IdPedido;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir o cupom: " + ex.Message.ToString());
                throw;
            }

        }

        private async Task AdicionaCupomUsoPedido(CupomAplicacaoRequest cupomEntrada, decimal valorTotal, AplicaCupomResponse response, Cupom cupomEncontrado)
        {
            var cupomUsoPedidoEncontrado = _cupomUsoPedidoRepository.BuscaPorCpfECupom(cupomEntrada.Cpf, cupomEncontrado.Id);

            if(cupomUsoPedidoEncontrado == null)
            {
                var _cupomUsoPedidoInserir = new CupomUsoPedido()
                {
                    Cpf = cupomEntrada.Cpf,
                    CupomId = cupomEncontrado.Id,
                    PedidoId = cupomEntrada.IdPedido, //A definir
                    DataCadastro = DateTime.Now,
                    ValorTotal = valorTotal,
                    ValorTotalComDesconto = response.ValorTotalComDesconto,
                    ValorCalculadoDesconto = response.ValorDescontoCalculado
                };

                _cupomUsoPedidoRepository.Add(_cupomUsoPedidoInserir);
                await _cupomUsoPedidoRepository.SaveChangesAsync();
            }
            else 
            {
                cupomUsoPedidoEncontrado.ValorTotal = valorTotal;
                cupomUsoPedidoEncontrado.ValorTotalComDesconto = response.ValorTotalComDesconto;
                cupomUsoPedidoEncontrado.ValorCalculadoDesconto = response.ValorDescontoCalculado;
                _cupomUsoPedidoRepository.Update(cupomUsoPedidoEncontrado);
                await _cupomUsoPedidoRepository.SaveChangesAsync();
            }
        }

        public async Task<ResultadoOperacao> CancelaCupom(CupomAplicacaoRequest cupomEntrada)
        {
            try
            {
                var cupomEncontrado = await _cupomRepository.GetByCodigo(cupomEntrada.CodigoCupom);
                if (cupomEncontrado == null)
                    throw new Exception($"O cupom informado não existe!");

                var cupomUsoPedidoEncontrado = _cupomUsoPedidoRepository.BuscaPorCpfECupom(cupomEntrada.Cpf, cupomEncontrado.Id);
                if(cupomUsoPedidoEncontrado != null)
                {
                    _cupomUsoPedidoRepository.Delete(cupomUsoPedidoEncontrado);
                    await _cupomUsoPedidoRepository.SaveChangesAsync();

                    return new ResultadoOperacao()
                    {
                        Message = $"O cupom {cupomEncontrado.CodigoCupom} foi cancelado com sucesso",
                        Success = true
                    };
                }

                return new ResultadoOperacao()
                {
                    Message = $"O cupom {cupomEncontrado.CodigoCupom} não foi cancelado!",
                    Success = false
                };
            } 
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao cancelar o cupom: " + ex.Message.ToString());
                throw;
            }
        }
    }
}