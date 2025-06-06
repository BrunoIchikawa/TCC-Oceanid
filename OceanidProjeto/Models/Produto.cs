using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbProduto")]
    public class Produto
    {
        [Key]
        [Column("idProd")]
        public int idProd { get; set; }

        [Column("codBar")]
        [StringLength(15, ErrorMessage = "O código de barras não pode exceder 15 caracteres")]
<<<<<<< HEAD
        public string codBar { get; set; }
=======
        public string? codBar { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("nomeProd")]
        [Required(ErrorMessage = "O nomePromocao do produto é obrigatório")]
        [StringLength(200, ErrorMessage = "O nomePromocao não pode exceder 200 caracteres")]
<<<<<<< HEAD
        public string nomeProd { get; set; }
=======
        public string? nomeProd { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("precoProd")]
        [Required(ErrorMessage = "O preço do produto é obrigatório")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre 0.01 e 999999.99")]
        public decimal precoProd { get; set; }

        [Column("qtdProd")]
        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
        public int qtdProd { get; set; }

        [Column("marcaProd")]
        [Required(ErrorMessage = "A marca é obrigatória")]
        [StringLength(50, ErrorMessage = "A marca não pode exceder 50 caracteres")]
<<<<<<< HEAD
        public string marcaProd { get; set; }
=======
        public string? marcaProd { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("descricaoProd")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição não pode exceder 200 caracteres")]
<<<<<<< HEAD
        public string descricaoProd { get; set; }
=======
        public string? descricaoProd { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [Column("idCategoria")]
        public int idCategoria { get; set; }

        [ForeignKey("idCategoria")]
<<<<<<< HEAD
        public Categoria categoria { get; set; }
=======
        public  Categoria? categoria { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        public ICollection<Promocao> Promocao { get; set; } = new HashSet<Promocao>();
        public ICollection<ClienteFavorito> ClienteFavoritos { get; set; } = new HashSet<ClienteFavorito>();
        public ICollection<ItemPedido> ItensPedidos { get; set; } = new HashSet<ItemPedido>();

        [NotMapped]
<<<<<<< HEAD
        public string imagemUrl
=======
        public string? imagemUrl
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            get
            {
                var imagePath = $"/img/produtos/img{idProd}.png";
                // Verifica se a imagem existe fisicamente (opcional, requer System.IO)
                // var webRootPath = _hostingEnvironment.WebRootPath; // Se quiser verificar fisicamente
                // var fullPath = Path.Combine(webRootPath, "img", "produtos", $"img{idProd}.png");
<<<<<<< HEAD
                // return File.Exists(fullPath) ? imagePath : "/img/produtos/sem-imagem.png";
=======
                // return File.Exists(fullPath)  imagePath : "/img/produtos/sem-imagem.png";
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

                // Ou simplesmente:
                return imagePath;
            }
        }
    }
}