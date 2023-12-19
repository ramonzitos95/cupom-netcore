using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cliqx.cupom.api
{
    [Table("cupom_limite_cpf")]
    public class CupomLimiteCpf
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        public string Cpf { get; set; }
        public string NomeCliente { get; set; }

        public int QuantidadeLimite { get; set; }
    }
}
