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
<<<<<<< HEAD
        public int idPedido { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Column("idProd")]
        public int idProd { get; set; }
=======
        public  int idPedido { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Column("idProd")]
        public  int idProd { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Column("quantidade")]
        [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório")]
        [Column("precoUnitario")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre 0.01 e 999999.99")]
        public decimal PrecoUnitario { get; set; }

        [ForeignKey("idPedido")]
<<<<<<< HEAD
        public Pedido pedido { get; set; }
=======
        public Pedido? pedido { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [ForeignKey("idProd")]
        public Produto produto { get; set; }
    }
}