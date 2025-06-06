using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbPedido")]
    public class Pedido
    {
        [Key]
        [Column("idPed")]
        public int idPed { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [Column("idEnd")]
        public int idEnd { get; set; }

        [Required(ErrorMessage = "O pagamento é obrigatório")]
        [Column("idPag")]
        public int idPag { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório")]
        [Column("idCliente")]
        public int idCliente { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória")]
        [Column("dataPed")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O total do pedido é obrigatório")]
        [Column("totalPed")]
        [Range(0.01, 999999.99, ErrorMessage = "O total deve estar entre 0.01 e 999999.99")]
        public decimal Total { get; set; }

        [ForeignKey("idEnd")]
        public Endereco endereco { get; set; }

        [ForeignKey("idPag")]
        public Pagamento pagamento { get; set; }

        [ForeignKey("idCliente")]
        public Cliente cliente { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new HashSet<ItemPedido>();
    }
}