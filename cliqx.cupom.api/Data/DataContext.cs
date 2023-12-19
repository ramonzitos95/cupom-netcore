using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace cliqx.cupom.api
{
    public class DataContext : DbContext
    {
        private readonly string _connString;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
        : base(options)
        {
            Configuration = configuration;
            _connString = Configuration.GetConnectionString("MariaDBContext");
        }

        public virtual DbSet<Cupom> Cupom { get; set; }
        public virtual DbSet<CupomLimiteCpf> CupomLimiteCpf { get; set; }
        public virtual DbSet<CupomUsoPedido> CupomUsoPedido { get; set; }
        public IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cupom>()
                .HasData(new[]
                {
                    new Cupom()
                    {
                        Id = 1,
                        CodigoCupom = "PRIMEIRACOMPRA",
                        Descricao = "Destinado a usuários de primeira compra",
                        DataValidade = DateTime.Now.AddYears(5),
                        DataCadastro = DateTime.Now,
                        PercentualDesconto = 30,
                        ValorDesconto = 0,
                        TipoCupom = TipoCupom.PRIMEIRA_COMPRA,
                        TipoDesconto = TipoDesconto.PERCENTUAL,
                        Usuario = "SNOG"
                    },
                    new Cupom()
                    {
                        Id = 2,
                        CodigoCupom = "ANIVERSARIO",
                        Descricao = "Destinado a quem faz aniversário",
                        DataValidade = DateTime.Now.AddYears(1),
                        DataCadastro = DateTime.Now,
                        PercentualDesconto = 0,
                        ValorDesconto = 10,
                        TipoCupom = TipoCupom.GERAL,
                        TipoDesconto = TipoDesconto.VALOR,
                        Usuario = "SNOG"
                    }
                });
            modelBuilder.Entity<CupomLimiteCpf>();
            modelBuilder.Entity<CupomUsoPedido>();
        }

    }
}
