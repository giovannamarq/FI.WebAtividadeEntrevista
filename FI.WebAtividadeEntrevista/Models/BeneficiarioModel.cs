using System;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de modelo do Beneficiário.
    /// </summary>
    public class BeneficiarioModel
    {
        /// <summary>
        /// ID do Beneficiário (0 para novo)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID do cliente ao qual o beneficiário pertence.
        /// /summary>
        [Required]
        public long IdCliente { get; set; }

        /// <summary>
        /// Cadastro de Pessoa Física.
        /// </summary>
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres (000.000.000-00).")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(50)]
        public string Nome { get; set; }
    }
}