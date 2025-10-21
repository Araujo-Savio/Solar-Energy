using System.ComponentModel.DataAnnotations;

namespace SolarEnergy.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome completo � obrigat�rio")]
        [Display(Name = "Nome Completo")]
        [StringLength(100, ErrorMessage = "O nome deve ter no m�ximo 100 caracteres")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "CPF")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF deve estar no formato 000.000.000-00")]
        public string? CPF { get; set; }

        [Display(Name = "CNPJ")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "CNPJ deve estar no formato 00.000.000/0000-00")]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "O e-mail � obrigat�rio")]
        [EmailAddress(ErrorMessage = "E-mail inv�lido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Telefone")]
        [Phone(ErrorMessage = "Telefone inv�lido")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "A senha � obrigat�ria")]
        [StringLength(100, ErrorMessage = "A senha deve ter pelo menos {2} caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirma��o de senha � obrigat�ria")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas n�o conferem")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Voc� deve aceitar os termos de uso")]
        [Display(Name = "Aceito os Termos de Uso e Pol�tica de Privacidade")]
        public bool AcceptTerms { get; set; }

        [Display(Name = "Tipo de Usu�rio")]
        public UserType UserType { get; set; } = UserType.Client;
    }
}