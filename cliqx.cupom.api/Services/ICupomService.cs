using cliqx.cupom.api.Models.RecargaPlus.Response;
using cliqx.cupom.api.Models.Request;
using cliqx.cupom.api.Models.Response;

namespace cliqx.cupom.api.services
{
    public interface ICupomService
    {
        Task<Cupom> InsereCupom(Cupom cupom);
        Task<Cupom> GetByCodigo(string codigo);
        Task<AplicaCupomResponse> AplicaCupom(CupomAplicacaoRequest cupom);
        Task<ResultadoOperacao> CancelaCupom(CupomAplicacaoRequest cupom);
    }
}
