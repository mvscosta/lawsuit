using Mc2Tech.Crosscutting.Enums;
using Mc2Tech.Crosscutting.Interfaces;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Mc2Tech.LawSuitsApi.DAL
{
    public class SituationDbContext : DbContext, IBaseDbContext
    {
        public SituationDbContext(DbContextOptions<SituationDbContext> options) : base(options)
        {
            if (base.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                base.Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SituationEntity>(cfg =>
            {
                cfg.ToTable("Situations");
                cfg.HasKey(e => e.Id);
                cfg.HasAlternateKey(e => e.ExternalReference);

                cfg.HasIndex(e => e.Name).IsUnique();

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.ExternalReference)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                cfg.Property(e => e.Description)
                    .HasMaxLength(1000);
                cfg.Property(e => e.IsClosed)
                    .IsRequired();
                cfg.Property(e => e.CreationDate)
                    .HasDefaultValueSql("getutcdate()");
                cfg.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("getutcdate()");
                cfg.Property(e => e.Status)
                    .HasDefaultValue(ObjectStatus.Active);

                cfg.HasData(this.DefaultData());
            });
        }

        private SituationEntity[] DefaultData()
        {
            return new SituationEntity[] {
                new SituationEntity { Id = new Guid("2349B7E9-F966-4159-8B8B-08D8A4840D3F"), Name = "Finalizado", IsClosed = true },
                new SituationEntity { Id = new Guid("FEA5977D-D634-475C-0A87-08D8A484DD71"), Name = "Arquivado", IsClosed = false },
                new SituationEntity { Id = new Guid("12CFD7D7-E71A-43C9-07E2-08D8A4850F73"), Name = "Em Recurso", IsClosed = false },
                new SituationEntity { Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), Name = "Em Andamento", IsClosed = false },
                new SituationEntity { Id = new Guid("E0CC0CAC-C0FB-4403-8E58-D48D279D50B0"), Name = "Desmembrado", IsClosed = true }
            };
        }

        public void Seed()
        {
            var db = base.Set<SituationEntity>();

            db.AddRange(DefaultData());

           if (this.ChangeTracker.HasChanges())
            {
                this.SaveChanges();
            }
        }
    }
}
