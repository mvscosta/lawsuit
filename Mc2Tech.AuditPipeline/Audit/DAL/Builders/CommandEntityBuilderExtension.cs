using Mc2Tech.Pipelines.Audit.Model.Audits;
using Microsoft.EntityFrameworkCore;

namespace Mc2Tech.Pipelines.Audit.DAL.Builders
{
    public static partial class EntityBuilderExtension
    {
        public static ModelBuilder CommandEntityBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommandEntity>(cfg =>
            {
                cfg.ToTable("Commands");
                cfg.HasKey(e => e.Id);

                cfg.HasIndex(e => e.CreatedOn);

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.Property(e => e.ExternalReference)
                    .IsRequired();
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
                cfg.Property(e => e.Payload)
                    .IsRequired();
                cfg.Property(e => e.Result);
                cfg.Property(e => e.CreatedOn)
                    .IsRequired();
                cfg.Property(e => e.CreatedBy)
                    .HasMaxLength(128);
                cfg.Property(e => e.ExecutionTime)
                    .IsRequired();
            });

            return modelBuilder;
        }
    }
}
