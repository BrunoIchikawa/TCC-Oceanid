using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbCliente")]
    public class Cliente
    {
        [Key]
        [Column("idCliente")]
        public int idCliente { get; set; }

        [Column("cpf")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas números")]
<<<<<<< HEAD
        public string cpf { get; set; }
=======
        public string? cpf { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("nomeCompleto")]
        [Required(ErrorMessage = "nomeCompleto é obrigatório")]
        [StringLength(200, ErrorMessage = "O nomePromocao não pode exceder 200 caracteres")]
<<<<<<< HEAD
        public string nomeCompleto { get; set; }
=======
        public string? nomeCompleto { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("senhaCliente")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "A senha não pode exceder 255 caracteres")]
<<<<<<< HEAD
        public string senhaCliente { get; set; }
=======
        public string? senhaCliente { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("emailCliente")]
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres")]
<<<<<<< HEAD
        public string emailCliente { get; set; }
=======
        public string? emailCliente { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        [Column("dataNasc")]
        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime dataNasc { get; set; }

        [Column("idEnd")]
<<<<<<< HEAD
        public int? idEnd { get; set; }

        [ForeignKey("idEnd")]
        public Endereco enderecoCli { get; set; }

=======
        public int idEnd { get; set; }

        [ForeignKey("idEnd")]
        public Endereco? enderecoCli { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        public ICollection<ClienteFavorito> ClienteFavoritos { get; set; } = new HashSet<ClienteFavorito>();
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
        public ICollection<Login> Login{ get; set; } = new HashSet<Login>();

    }
}