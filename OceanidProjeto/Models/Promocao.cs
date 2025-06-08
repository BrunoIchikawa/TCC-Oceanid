using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbPromocao")]
    public class Promocao
    {
        [Key]
        [Column("idPromocao")]
        public  int idPromocao { get; set; }

        [Required(ErrorMessage = "O nomePromocao da promoção é obrigatório")]
        [Column("nomePromocao")]
        [StringLength(100, ErrorMessage = "O nomePromocao não pode exceder 100 caracteres")]
        public  string? nomePromocao { get; set; }

        [Required(ErrorMessage = "O tipo de desconto é obrigatório")]
        [Column("tipoDesconto")]
        [StringLength(20, ErrorMessage = "O tipo de desconto não pode exceder 20 caracteres")]
        public  string? tipoDesconto { get; set; } 

        [Column("valorDesconto")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor do desconto deve ser positivo")]
        [CustomValidation(typeof(Promocao), "ValidatevalorDesconto")]
        public decimal? valorDesconto { get; set; }

        [Column("precoPromocional")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        [CustomValidation(typeof(Promocao), "ValidateprecoPromocional")]
        public decimal? precoPromocional { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        [Column("dataInicio")]
        public DateTime dataInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória")]
        [Column("dataFim")]
        [CustomValidation(typeof(Promocao), "ValidatedataFim")]
        public DateTime dataFim { get; set; }

        [Required]
        [Column("ativa")]
        public bool ativa { get; set; } = true;

        [Column("limitePorCliente")]
        [Range(1, int.MaxValue, ErrorMessage = "O limite por cliente deve ser pelo menos 1")]
        public int limitePorCliente { get; set; }

        [Column("idProd")]
        [PromocaoTypeValidation]
        public int idProd { get; set; }

        [ForeignKey("idProd")]
        public Produto produto { get; set; }

        [Column("idCategoria")]
        [PromocaoTypeValidation]
        public int idCategoria { get; set; }

        [ForeignKey("idCategoria")]
        public Categoria? categoria { get; set; }

        public static ValidationResult ValidatedataFim(DateTime dataFim, ValidationContext context)
        {
            var Promocao = (Promocao)context.ObjectInstance;

            if (dataFim <= Promocao.dataInicio)
            {
                return new ValidationResult("A data de término deve ser posterior à data de início");
            }

            #pragma warning disable CS8603 
            return ValidationResult.Success;
        }

        public static ValidationResult ValidatevalorDesconto(object value, ValidationContext context)
        {
            var Promocao = (Promocao)context.ObjectInstance;

            if (Promocao.tipoDesconto == "Percentual")
            {
                if (Promocao.valorDesconto < 0 || Promocao.valorDesconto > 100)
                {
                    return new ValidationResult("Para desconto percentual, o valor deve estar entre 0 e 100");
                }
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateprecoPromocional(object value, ValidationContext context)
        {
            var Promocao = (Promocao)context.ObjectInstance;

            if (Promocao.tipoDesconto == "Valor Fixo" && Promocao.precoPromocional == 0)
            {
                return new ValidationResult("Para desconto de valor fixo, o preço promocional é obrigatório");
            }

            return ValidationResult.Success;
        }

        public class PromocaoTypeValidationAttribute : ValidationAttribute
        {
            #pragma warning disable CS8765 // A nulidade do tipo de parâmetro não corresponde ao membro substituído (possivelmente devido a atributos de nulidade).
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var Promocao = (Promocao)validationContext.ObjectInstance;

                if (Promocao.idPromocao == 0 && Promocao.idPromocao == 0)
                {
                    return new ValidationResult("Selecione um produto OU uma categoria");
                }
                
                if (Promocao.idPromocao != 0 && Promocao.idPromocao != 0)
                {
                    return new ValidationResult("Selecione apenas um produto OU uma categoria");
                }

                return ValidationResult.Success;
            }
        }
    }
}