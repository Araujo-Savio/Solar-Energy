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
                entity.Property(e => e.CompanyLegalName).HasMaxLength(120);
                entity.Property(e => e.CompanyTradeName).HasMaxLength(120);
                entity.Property(e => e.StateRegistration).HasMaxLength(30);
                entity.Property(e => e.CompanyPhone).HasMaxLength(20);
                entity.Property(e => e.CompanyWebsite).HasMaxLength(200);
                entity.Property(e => e.CompanyDescription).HasMaxLength(500);
                entity.Property(e => e.ResponsibleName).HasMaxLength(100);
                entity.Property(e => e.ResponsibleCPF).HasMaxLength(14);
                entity.Property(e => e.Location).HasMaxLength(120);
                entity.Property(e => e.ProfileImagePath).HasMaxLength(260);

                // Índices únicos
                entity.HasIndex(e => e.CPF).IsUnique().HasFilter("[CPF] IS NOT NULL");
                entity.HasIndex(e => e.CNPJ).IsUnique().HasFilter("[CNPJ] IS NOT NULL");
            });
        }
    }
}
