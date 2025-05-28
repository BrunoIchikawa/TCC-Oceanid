using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbItemPedido")]
    public class ItemPedido
    {
        [Key]
        [Column("idItemPedido")]
        public int idItemPedido { get; set; }

        [Required(ErrorMessage = "O ID do pedido é obrigatório")]
        [Column("idPedido")]
        public  int idPedido { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Column("idProd")]
        public  int idProd { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Column("quantidade")]
        [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório")]
        [Column("precoUnitario")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre 0.01 e 999999.99")]
        public decimal PrecoUnitario { get; set; }

        [ForeignKey("idPedido")]
        public Pedido? pedido { get; set; }

        [ForeignKey("idProd")]
        public Produto produto { get; set; }
    }
}