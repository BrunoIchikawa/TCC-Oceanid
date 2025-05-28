using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbAdm")]
    public class Adm
    {
        [Key]
        [Column("idAdm")]
        public int idAdm { get; set; }

        [Required(ErrorMessage = "O nomePromocao do administrador é obrigatório")]
        [Column("nomeAdm")]
        [StringLength(70, ErrorMessage = "O nomePromocao não pode exceder 70 caracteres")]
        public  string? nomeAdm { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [Column("senhaAdm")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "A senha não pode exceder 255 caracteres")]
        public  string? senhaAdm { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [Column("emailAdm")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres")]
        public  string? emailAdm { get; set; }
        public ICollection<Login> Login { get; set; } = new HashSet<Login>();

    }
}