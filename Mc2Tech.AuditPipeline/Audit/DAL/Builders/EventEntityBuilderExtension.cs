using Mc2Tech.Pipelines.Audit.Model.Audits;
using Microsoft.EntityFrameworkCore;

namespace Mc2Tech.Pipelines.Audit.DAL.Builders
{
    public static partial class EntityBuilderExtension
    {
        public static ModelBuilder EventEntityBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntity>(cfg =>
            {
                cfg.ToTable("Events");
                cfg.HasKey(e => e.Id);

                cfg.HasIndex(e => e.ExternalReference);

                cfg.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                cfg.HasOne<CommandEntity>()
                    .WithMany()
                    .HasForeignKey(e => e.CommandId)
                    .IsRequired();
                cfg.Property(e => e.ExternalReference)
                    .IsRequired();
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
                cfg.Property(e => e.Payload)
                    .IsRequired();
                cfg.Property(e => e.CreatedOn)
                    .IsRequired();
                cfg.Property(e => e.CreatedBy)
                    .HasMaxLength(128);
            });

            return modelBuilder;
        }
    }
}
