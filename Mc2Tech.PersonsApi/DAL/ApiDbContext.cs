using Mc2Tech.Crosscutting.Enums;
using Mc2Tech.Crosscutting.Interfaces;
using Mc2Tech.PersonsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Mc2Tech.PersonsApi.DAL
{
    public class ApiDbContext : DbContext, IBaseDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            if (base.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                base.Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PersonEntity>(cfg =>
            {
                cfg.ToTable("Persons");
                cfg.HasKey(e => e.Id);
                cfg.HasAlternateKey(e => e.ExternalReference);

                cfg.HasIndex(e => e.Cpf).IsUnique();

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.ExternalReference)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(11);
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
                cfg.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(400);

                cfg.Property(e => e.CreationDate)
                    .HasDefaultValueSql("getutcdate()");
                cfg.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("getutcdate()");
                cfg.Property(e => e.Status)
                    .HasDefaultValue(ObjectStatus.Active);

            });

            base.OnModelCreating(builder);
        }

        public void Seed()
        {
            if (this.ChangeTracker.HasChanges())
            {
                this.SaveChanges();
            }
        }
    }
}
