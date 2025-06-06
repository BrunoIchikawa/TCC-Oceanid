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
<<<<<<< HEAD
        public int idCliente { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Column("idProd")]
        public int idProd { get; set; }
=======
        public  int idCliente { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Column("idProd")]
        public  int idProd { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("ativo")]
        public bool ativo { get; set; } = true;

        [ForeignKey("idCliente")]
<<<<<<< HEAD
        public required Cliente cliente { get; set; }

        [ForeignKey("idProd")]
        public required Produto produto { get; set; }
=======
        public Cliente cliente { get; set; }

        [ForeignKey("idProd")]
        public Produto produto { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
    }
}