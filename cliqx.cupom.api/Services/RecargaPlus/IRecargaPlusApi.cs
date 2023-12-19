using cliqx.cupom.api.Models.RecargaPlus.Response;
using Refit;

namespace cliqx.cupom.api.Services.RecargaPlus
{
    public interface IRecargaPlusApi
    {
        [Get("/pedido/{idPedido}")]
        public Task<ApiResponse<PedidoResponse>> BuscaPedidoPorId(string idPedido);

        [Get("/pedido/ObterPedidosPorCpf/{cpf}/{statusPedido}")]
        public Task<ApiResponse<List<PedidoResponse>>> BuscaPedidosPorCpfEStatus(string cpf, int statusPedido);
    }
}
