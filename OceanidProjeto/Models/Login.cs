using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbLogin")]
    public class Login
    {
        [Key]
        [Column("idLogin")]
        public int idLogin { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]

        [Column("idCliente")]
        public int idCliente { get; set; }
        
        [Column("idAdm")]
        public int idAdm{ get; set; }

        [ForeignKey("idCliente")]
        public Cliente cliente { get; set; }

        [ForeignKey("idAdm")]
        public Adm Adm { get; set; }
    }
}