using Mc2Tech.Crosscutting.Enums;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2Tech.LawSuitsApi.DAL.Builders
{
    public class LawSuitEntityBuilder : IEntityTypeConfiguration<LawSuitEntity>
    {
        public void Configure(EntityTypeBuilder<LawSuitEntity> builder)
        {
            builder.ToTable("LawSuits");
            builder.HasKey(e => e.Id);
            builder.HasAlternateKey(e => e.ExternalReference);

            builder.HasIndex(e => e.UnifiedProcessNumber).IsUnique();

            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(e => e.ExternalReference)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(e => e.UnifiedProcessNumber)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(e => e.ClientPhysicalFolder)
                .HasMaxLength(50);
            builder.Property(e => e.SituationId)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(1000);

            builder.HasOne(e => e.ParentLawSuit)
                .WithMany(p => p.ChildLawSuits)
                .HasForeignKey(d => d.ParentLawSuitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.CreationDate)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(e => e.Status)
                .HasDefaultValue(ObjectStatus.Active);
        }
    }
}
