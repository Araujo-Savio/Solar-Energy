using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarEnergy.Models;

namespace SolarEnergy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurações adicionais do modelo podem ser adicionadas aqui
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CPF).HasMaxLength(14);
                entity.Property(e => e.CNPJ).HasMaxLength(18);
                entity.Property(e => e.Phone).HasMaxLength(20);
                
                // Índices únicos
                entity.HasIndex(e => e.CPF).IsUnique().HasFilter("[CPF] IS NOT NULL");
                entity.HasIndex(e => e.CNPJ).IsUnique().HasFilter("[CNPJ] IS NOT NULL");
            });
        }
    }
}