using cliqx.cupom.api.Models.RecargaPlus.Response;
using cliqx.cupom.api.Models.Request;
using cliqx.cupom.api.Repositories.Contracts;
using cliqx.cupom.api.services;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.cupom.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupomController : ControllerBase
    {
        private ILogger<CupomController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICupomRepository _cupomRepository;
        private readonly ICupomService _cupomService;

        public CupomController(
            ILogger<CupomController> logger,
            IConfiguration configuration,
            ICupomRepository cupomRepository,
            ICupomService cupomService)
        {
            _logger = logger;
            _cupomRepository = cupomRepository;
            _configuration = configuration;
            _cupomService = cupomService;
        }

        [HttpGet("GET")]
        public string Get()
        {
            try
            {
                var ret = "API FUNCIONANDO";
                return ret;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        [HttpPost]
        public async Task<ActionResult<Cupom>> AddCupom(Cupom cupom)
        {
            try
            {
                Console.WriteLine($"Adicionando cupom {cupom.CodigoCupom}");

                if (cupom is null)
                    return StatusCode(404, "O cupom está nulo!");

                var cupomSalvo = await _cupomService.InsereCupom(cupom);

                if (cupomSalvo is null)
                    return StatusCode(204, $"Erro ao salvar no banco o cupom {cupom.CodigoCupom}!");

                return Ok(cupom);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException) return StatusCode(501);
                var err = $"Error AddCupom: {e.Message}";
                _logger.LogError(e, err);
                return BadRequest(err);
            }
        }

        [HttpGet("BuscaPorCodigo/{codigo}")]
        public async Task<ActionResult<Cupom>> BuscaCupomPorCodigo(string codigo)
        {
            try
            {
                var cupom = await _cupomService.GetByCodigo(codigo);

                if (cupom is null)
                    return StatusCode(204, $"{cupom.CodigoCupom} Cupom não encontrado!");

                return Ok(cupom);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException) return StatusCode(501);
                var err = $"Error BuscaPorCodigo: {e.Message}";
                _logger.LogError(e, err);
                return BadRequest(err);
            }
        }

        [HttpPost("AplicaCupom")]
        public async Task<ActionResult<Cupom>> AplicaCupom(CupomAplicacaoRequest cupomAplicacaoModel)
        {
            try
            {
                var cupom = await _cupomService.AplicaCupom(cupomAplicacaoModel);
                return Ok(cupom);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException) return StatusCode(501);
                var err = $"Error AplicaCupom: {e.Message}";
                _logger.LogError(e, err);
                return BadRequest(err);
            }
        }

        [HttpPost("CancelaCupom")]
        public async Task<ActionResult<ResultadoOperacao>> CancelaCupom(CupomAplicacaoRequest cupomAplicacaoModel)
        {
            try
            {
                var cupom = await _cupomService.CancelaCupom(cupomAplicacaoModel);
                return Ok(cupom);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException) return StatusCode(501);
                var err = $"Error CancelaCupom: {e.Message}";
                _logger.LogError(e, err);
                return BadRequest(err);
            }
        }
    }
}
