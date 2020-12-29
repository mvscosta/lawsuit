using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mc2Tech.Pipelines.Audit.DAL
{
    public class AuditDbContextFactory : IDesignTimeDbContextFactory<AuditDbContext>
    {
        public AuditDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuditDbContext>();
            optionsBuilder.UseSqlServer("server=localhost;database=ApiDb;Integrated Security=True;");

            return new AuditDbContext(optionsBuilder.Options);
        }
    }
}
