using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cliqx.cupom.api
{
    [Table("cupom")]
    public class Cupom
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string CodigoCupom { get; set; }

        public string Descricao { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required]
        public DateTime DataValidade { get; set; }

        [Required]
        public TipoCupom TipoCupom { get; set; }

        [Required]
        public TipoDesconto TipoDesconto { get; set; }

        public decimal ValorDesconto { get; set; }

        public decimal PercentualDesconto { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Usuario { get; set; }
    }
}
