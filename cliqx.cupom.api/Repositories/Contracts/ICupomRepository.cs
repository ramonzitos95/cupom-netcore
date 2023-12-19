namespace cliqx.cupom.api.Repositories.Contracts
{
    public interface ICupomRepository : IRepositoryBase<Cupom>
    {
        public Task<Cupom> CreateAsync(Cupom cupom);
        public Task<Cupom> GetByCodigo(string codigo);
        
    }
}
