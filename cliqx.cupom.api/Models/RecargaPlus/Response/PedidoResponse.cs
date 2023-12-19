using Newtonsoft.Json;

namespace cliqx.cupom.api.Models.RecargaPlus.Response
{
    public class PedidoResponse
    {
        [JsonProperty("pedidoId")]
        public int PedidoId { get; set; }

        [JsonProperty("clienteId")]
        public int? ClienteId { get; set; }

        [JsonProperty("lojaId")]
        public int LojaId { get; set; }

        [JsonProperty("tipoPedidoId")]
        public int TipoPedidoId { get; set; }

        [JsonProperty("valorTotal")]
        public decimal? ValorTotal { get; set; }

        [JsonProperty("itens")]
        public List<ItemPedidoResponse> Itens { get; set; }

        [JsonProperty("dataCadastro")]
        public DateTime? DataCadastro { get; set; }

        [JsonProperty("statusPedidoId")]
        public long? StatusPedidoId { get; set; }

        [JsonProperty("cliente")]
        public ClienteResponse Cliente { get; set; }
    }
}
