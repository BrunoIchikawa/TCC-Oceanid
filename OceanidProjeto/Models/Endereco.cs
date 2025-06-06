using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbEndereco")]
    public class Endereco
    {
        [Key]
        [Column("idEnd")]
        public int idEnd { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [Column("cepEnd")]
        [StringLength(10, ErrorMessage = "CEP não pode exceder 10 caracteres")]
<<<<<<< HEAD
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP inválido")]
        public string Cep { get; set; }
=======
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido")]
        public  string? cepEnd { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Required(ErrorMessage = "Número é obrigatório")]
        [Column("numeroEnd")]
        [Range(1, int.MaxValue, ErrorMessage = "Número inválido")]
<<<<<<< HEAD
        public int Numero { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [Column("logradouro")]
        [StringLength(250, ErrorMessage = "Logradouro não pode exceder 250 caracteres")]
        public string Logradouro { get; set; }

        [Column("complemento")]
        [StringLength(100, ErrorMessage = "Complemento não pode exceder 100 caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        [Column("bairro")]
        [StringLength(100, ErrorMessage = "Bairro não pode exceder 100 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [Column("estado")]
        [StringLength(100, ErrorMessage = "Estado não pode exceder 100 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [Column("cidade")]
        [StringLength(100, ErrorMessage = "Cidade não pode exceder 100 caracteres")]
        public string Cidade { get; set; }
=======
        public  int numeroEnd { get; set; }

        [Required(ErrorMessage = "logradouro é obrigatório")]
        [Column("logradouro")]
        [StringLength(250, ErrorMessage = "logradouro não pode exceder 250 caracteres")]
        public  string? logradouro { get; set; }

        [Column("complemento")]
        [StringLength(100, ErrorMessage = "complemento não pode exceder 100 caracteres")]
        public  string? complemento { get; set; }

        [Required(ErrorMessage = "bairro é obrigatório")]
        [Column("bairro")]
        [StringLength(100, ErrorMessage = "bairro não pode exceder 100 caracteres")]
        public  string? bairro { get; set; }

        [Required(ErrorMessage = "estado é obrigatório")]
        [Column("estado")]
        [StringLength(100, ErrorMessage = "estado não pode exceder 100 caracteres")]
        public  string? estado { get; set; }

        [Required(ErrorMessage = "cidade é obrigatória")]
        [Column("cidade")]
        [StringLength(100, ErrorMessage = "cidade não pode exceder 100 caracteres")]
        public  string? cidade { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        public ICollection<Cliente> Clientes { get; set; } = new HashSet<Cliente>();
        public ICollection<Pedido> Pedidos{ get; set; } = new HashSet<Pedido>();
    }
}