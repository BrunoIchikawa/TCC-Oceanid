using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbCategoria")]
    public class Categoria
    {
        [Key]
        [Column("idCategoria")]
        public int idCategoria { get; set; }

        [Column("nomeCategoria")]
        [Required(ErrorMessage = "O nomePromocao da categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "O nomePromocao não pode exceder 100 caracteres")]
<<<<<<< HEAD
        public string nomeCategoria { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
        public virtual ICollection<Promocao> Promocao { get; set; }
=======
        public string? nomeCategoria { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
        public ICollection<Promocao>? Promocao { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
    }
}