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
        public string codBar { get; set; }

        [Column("nomeProd")]
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string nomeProd { get; set; }

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
        public string marcaProd { get; set; }

        [Column("descricaoProd")]
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição não pode exceder 200 caracteres")]
        public string descricaoProd { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [Column("idCategoria")]
        public int idCategoria { get; set; }

        [NotMapped]
        public string imagemUrl { get; set; }


        [ForeignKey("idCategoria")]
        public Categoria categoria { get; set; }

        public ICollection<Promocoes> Promocoes { get; set; } = new HashSet<Promocoes>();
        public ICollection<ClienteFavorito> ClienteFavoritos { get; set; } = new HashSet<ClienteFavorito>();
        public ICollection<ItemPedido> ItensPedidos { get; set; } = new HashSet<ItemPedido>();
    }
}