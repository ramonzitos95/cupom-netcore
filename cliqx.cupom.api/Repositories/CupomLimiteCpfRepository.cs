using cliqx.cupom.api.Repositories.Contracts;

namespace cliqx.cupom.api.Repositories
{
    public class CupomLimiteCpfRepository : RepositoryBase<CupomLimiteCpf>, ICupomLimiteCpfRepository
    {
        public CupomLimiteCpfRepository(DataContext _context) : base(_context)
        {
        }

        public int BuscaQuantidadeCuponsPorCpf(string cpf)
        {
            return Context.CupomLimiteCpf
                .Where(x => x.Cpf == cpf)
                .Select(x => x.QuantidadeLimite)
                .FirstOrDefault();
        }

      
    }
}
