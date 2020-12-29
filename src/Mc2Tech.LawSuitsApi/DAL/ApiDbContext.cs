using Mc2Tech.LawSuitsApi.DAL.Builders;
using Microsoft.EntityFrameworkCore;

namespace Mc2Tech.LawSuitsApi.DAL
{
    public class ApiDbContext : DbContext
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
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LawSuitEntityBuilder());
            //builder.ApplyConfiguration(new LawSuitResponsibleEntityBuilder());
        }
    }
}