namespace cliqx.cupom.api.Models.Request
{
    public class CupomAplicacaoRequest
    {
        public string CodigoCupom { get; set; }
        public long IdPedido { get; set; }
        public string Cpf { get; set; }
        public decimal ValorACalcular { get; set; }
        public decimal ValorCalculado { get; set; }
    }
}
