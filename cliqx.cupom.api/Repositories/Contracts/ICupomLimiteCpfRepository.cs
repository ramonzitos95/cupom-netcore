namespace cliqx.cupom.api.Repositories.Contracts
{
    public interface ICupomLimiteCpfRepository : IRepositoryBase<CupomLimiteCpf>
    {
        public int BuscaQuantidadeCuponsPorCpf(string cpf);
    }
}
