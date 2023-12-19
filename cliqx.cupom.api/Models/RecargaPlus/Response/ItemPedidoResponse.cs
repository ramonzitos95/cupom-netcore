using Newtonsoft.Json;

namespace cliqx.cupom.api.Models.RecargaPlus.Response
{
    public class ItemPedidoResponse
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("valorUnitario")]
        public decimal ValorUnitario { get; set; }

        [JsonProperty("descricoes")]
        public List<DescricaoResponse> Descricoes { get; set; }

        public ItemPedidoResponse()
        {
            Descricoes = new List<DescricaoResponse>();
        }
    }
}
