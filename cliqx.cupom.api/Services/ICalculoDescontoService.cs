namespace cliqx.cupom.api.Services
{
    public interface ICalculoDescontoService
    {
        decimal CalculaValorDesconto(TipoDesconto tipoDesconto, decimal valorDesconto, decimal valor = 0, decimal percentualDesconto = 0);
    }
}
