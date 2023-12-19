using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cliqx.cupom.api
{
    [Table("cupom_uso_pedido")]
    public class CupomUsoPedido
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        public long PedidoId { get; set; }
        public long CupomId { get; set; }
        public string Cpf { get; set; }

        public decimal ValorTotal { get; set; }
        public decimal ValorTotalComDesconto { get; set; }
        public decimal ValorCalculadoDesconto { get; set; }

    }
}
