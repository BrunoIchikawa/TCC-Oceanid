using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanidProjeto.Models
{
    [Table("tbPromocoes")]
    public class Promocoes
    {
        [Key]
        [Column("idPromocoes")]
        public int idPromocoes { get; set; }

        [Required(ErrorMessage = "O nome da promoção é obrigatório")]
        [Column("nomePromocoes")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo de desconto é obrigatório")]
        [Column("tipoDesconto")]
        [StringLength(20, ErrorMessage = "O tipo de desconto não pode exceder 20 caracteres")]
        public string TipoDesconto { get; set; } // Agora como string

        [Column("valorDesconto")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor do desconto deve ser positivo")]
        [CustomValidation(typeof(Promocoes), "ValidateValorDesconto")]
        public decimal? ValorDesconto { get; set; }

        [Column("precoPromocional")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        [CustomValidation(typeof(Promocoes), "ValidatePrecoPromocional")]
        public decimal? PrecoPromocional { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        [Column("dataInicio")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória")]
        [Column("dataFim")]
        [CustomValidation(typeof(Promocoes), "ValidateDataFim")]
        public DateTime DataFim { get; set; }

        [Required]
        [Column("ativa")]
        public bool Ativa { get; set; } = true;

        [Column("limitePorCliente")]
        [Range(1, int.MaxValue, ErrorMessage = "O limite por cliente deve ser pelo menos 1")]
        public int? LimitePorCliente { get; set; }

        [Column("idProd")]
        [PromocoesTypeValidation]
        public int? idProd { get; set; }

        [ForeignKey("idProd")]
        public Produto produto { get; set; }

        [Column("idCategoria")]
        [PromocoesTypeValidation]
        public int? idCategoria { get; set; }

        [ForeignKey("idCategoria")]
        public Categoria categoria { get; set; }

        public static ValidationResult ValidateDataFim(DateTime dataFim, ValidationContext context)
        {
            var Promocoes = (Promocoes)context.ObjectInstance;

            if (dataFim <= Promocoes.DataInicio)
            {
                return new ValidationResult("A data de término deve ser posterior à data de início");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateValorDesconto(object value, ValidationContext context)
        {
            var Promocoes = (Promocoes)context.ObjectInstance;

            if (Promocoes.TipoDesconto == "Percentual" && Promocoes.ValorDesconto.HasValue)
            {
                if (Promocoes.ValorDesconto < 0 || Promocoes.ValorDesconto > 100)
                {
                    return new ValidationResult("Para desconto percentual, o valor deve estar entre 0 e 100");
                }
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidatePrecoPromocional(object value, ValidationContext context)
        {
            var Promocoes = (Promocoes)context.ObjectInstance;

            if (Promocoes.TipoDesconto == "Valor Fixo" && !Promocoes.PrecoPromocional.HasValue)
            {
                return new ValidationResult("Para desconto de valor fixo, o preço promocional é obrigatório");
            }

            return ValidationResult.Success;
        }

        public class PromocoesTypeValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var Promocoes = (Promocoes)validationContext.ObjectInstance;

                if (Promocoes.idPromocoes == null && Promocoes.idPromocoes== null)
                {
                    return new ValidationResult("Selecione um produto OU uma categoria");
                }

                if (Promocoes.idPromocoes != null && Promocoes.idPromocoes != null)
                {
                    return new ValidationResult("Selecione apenas um produto OU uma categoria");
                }

                return ValidationResult.Success;
            }
        }
    }
}