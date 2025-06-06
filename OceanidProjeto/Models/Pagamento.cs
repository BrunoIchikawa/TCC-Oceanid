using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    public enum StatusPagamento
    {
        Pago,
        Pendente,
        NaoRealizado
    }

    [Table("tbPagamento")]
    public class Pagamento
    {
        [Key]
        [Column("idPag")]
        public int idPag { get; set; }

        [Required(ErrorMessage = "O status do pagamento é obrigatório")]
        [Column("statusPag")]
        public StatusPagamento status { get; set; } = StatusPagamento.Pendente;

        [Required(ErrorMessage = "O método de pagamento é obrigatório")]
        [Column("metodoPag")]
        [StringLength(50, ErrorMessage = "O método de pagamento não pode exceder 50 caracteres")]
<<<<<<< HEAD
        public string metodo { get; set; }
=======
        public string? metodo { get; set; }
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
    }
}