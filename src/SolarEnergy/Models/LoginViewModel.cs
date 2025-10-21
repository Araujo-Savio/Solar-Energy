using System.ComponentModel.DataAnnotations;

namespace SolarEnergy.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail � obrigat�rio")]
        [EmailAddress(ErrorMessage = "E-mail inv�lido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha � obrigat�ria")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembrar de mim")]
        public bool RememberMe { get; set; }
    }
}