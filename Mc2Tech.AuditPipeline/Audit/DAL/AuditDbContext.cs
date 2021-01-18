using Mc2Tech.Pipelines.Audit.DAL.Builders;
using Microsoft.EntityFrameworkCore;

namespace Mc2Tech.Pipelines.Audit.DAL
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options) 
            : base(options)
        {
            if (base.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                base.Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.CommandEntityBuilder();
            modelBuilder.EventEntityBuilder();
        }
    }
}
