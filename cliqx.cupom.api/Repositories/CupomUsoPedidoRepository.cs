using cliqx.cupom.api.Repositories.Contracts;

namespace cliqx.cupom.api.Repositories
{
    public class CupomUsoPedidoRepository : RepositoryBase<CupomUsoPedido>, ICupomUsoPedidoRepository
    {
        public CupomUsoPedidoRepository(DataContext _context) : base(_context)
        {
        }

        public CupomUsoPedido BuscaPorCpfECupom(string cpf, long idCupom)
        {
            return Context.CupomUsoPedido
                .Where(f => f.Cpf == cpf)
                .Where(x => x.CupomId == idCupom)
                .First();
        }

        public CupomUsoPedido BuscaPorPedidoECupom(long idPedido, long idCupom)
        {
            return Context.CupomUsoPedido
                .Where(f => f.PedidoId == idPedido)
                .Where(x => x.CupomId == idCupom)
                .First();
        }

        public int QuantidadeCuponsUsadosParaOCpf(string cpf)
        {
            return Context.CupomUsoPedido
                .Where(x => x.Cpf == cpf)
                .Count();
        }
    }
}
