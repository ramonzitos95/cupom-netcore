using cliqx.cupom.api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace cliqx.cupom.api.Repositories
{
    public class CupomRepository : RepositoryBase<Cupom>, ICupomRepository
    {
        public CupomRepository(DataContext _context) : base(_context)
        {
        }

        public Task<Cupom> CreateAsync(Cupom cupom)
        {
            throw new NotImplementedException();
        }

        public async Task<Cupom> GetByCodigo(string codigo)
        {
            var cupom = await Context.Cupom.FirstOrDefaultAsync(x => x.CodigoCupom == codigo);
            return cupom;
        }

       
    }
}
