namespace cliqx.cupom.api.Models.Response
{
    public class AplicaCupomResponse
    {
        public string CodigoCupom { get; set; }
        public long IdPedido { get; set; }
        public string Cpf { get; set; }
        public decimal ValorDescontoCalculado { get; set; }
        public decimal ValorTotalComDesconto { get; set; }
    }
}
