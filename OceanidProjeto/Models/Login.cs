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

        [Required(ErrorMessage = "O nomePromocao da categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "O nomePromocao não pode exceder 100 caracteres")]
<<<<<<< HEAD

=======
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        [Column("idCliente")]
        public int idCliente { get; set; }
        
        [Column("idAdm")]
<<<<<<< HEAD
        public int idAdm{ get; set; }
=======
        public int idAdm { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [ForeignKey("idCliente")]
        public Cliente cliente { get; set; }

        [ForeignKey("idAdm")]
<<<<<<< HEAD
        public Adm Adm { get; set; }
=======
        public Adm? Adm { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
    }
}