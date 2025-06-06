using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbClienteFavoritos")]
    public class ClienteFavorito
    {
        [Key]
        [Column("idClienteFav")]
        public int idClienteFav { get; set; }

        [Required(ErrorMessage = "O ID do cliente é obrigatório")]
        [Column("idCliente")]
        public int idCliente { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Column("idProd")]
        public int idProd { get; set; }

        [Column("ativo")]
        public bool ativo { get; set; } = true;

        [ForeignKey("idCliente")]
        public required Cliente cliente { get; set; }

        [ForeignKey("idProd")]
        public required Produto produto { get; set; }
    }
}