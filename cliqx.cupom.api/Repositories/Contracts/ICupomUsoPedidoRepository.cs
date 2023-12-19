namespace cliqx.cupom.api.Repositories.Contracts
{
    public interface ICupomUsoPedidoRepository : IRepositoryBase<CupomUsoPedido>
    {
        public int QuantidadeCuponsUsadosParaOCpf(string cpf);
        public CupomUsoPedido BuscaPorCpfECupom(string cpf, long idCupom);
        CupomUsoPedido BuscaPorPedidoECupom(long idPedido, long idCupom);
    }
}
