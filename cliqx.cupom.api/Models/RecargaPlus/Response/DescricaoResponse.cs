using Newtonsoft.Json;

namespace cliqx.cupom.api.Models.RecargaPlus.Response
{
    public class DescricaoResponse
    {
        [JsonProperty("chave")]
        public string Chave { get; set; }

        [JsonProperty("valor")]
        public string? Valor { get; set; }

        [JsonProperty("exibir")]
        public string Exibir { get; set; } = "N";

        [JsonProperty("posicao")]
        public int Posicao { get; set; } = 0;

        [JsonProperty("titulo")]
        public string Titulo { get; set; }
    }
}
