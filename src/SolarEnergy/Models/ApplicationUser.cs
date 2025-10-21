using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SolarEnergy.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nome Completo")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "CPF")]
        [StringLength(14)]
        public string? CPF { get; set; }

        [Display(Name = "CNPJ")]
        [StringLength(18)]
        public string? CNPJ { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public UserType UserType { get; set; } = UserType.Client;

        [Display(Name = "Data de Cadastro")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }

    public enum UserType
    {
        Client = 1,
        Company = 2,
        Administrator = 3
    }
}