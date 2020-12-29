using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2Tech.LawSuitsApi.DAL.Builders
{
    public class LawSuitResponsibleEntityBuilder : IEntityTypeConfiguration<LawSuitResponsibleEntity>
    {
        public void Configure(EntityTypeBuilder<LawSuitResponsibleEntity> builder)
        {
            builder.ToTable("LawSuitResponsibles");
            builder.HasKey(e => e.Id);
            builder.HasAlternateKey(e => e.ExternalReference);

            builder.HasIndex(e => e.LawSuitId);

            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Ignore(e => e.ExternalReference);

            builder.Property(e => e.PersonId)
                .IsRequired();

            builder.HasOne(e => e.LawSuit)
                .WithMany(a => a.LawSuitResponsibles)
                .HasForeignKey(d => d.LawSuitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.CreationDate)
                .HasDefaultValueSql("getdate()");
            builder.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("getdate()");
            builder.Property(e => e.Status)
                .HasDefaultValue(1);
        }
    }
}
