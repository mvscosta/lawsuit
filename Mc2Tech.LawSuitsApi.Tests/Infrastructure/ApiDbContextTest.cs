using Mc2Tech.Crosscutting.Interfaces;
using Mc2Tech.LawSuitsApi.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Mc2Tech.LawSuitsApi.Tests.Infrastructure
{
    public sealed class DbContextTest<T> where T : DbContext, IBaseDbContext
    {
        private readonly Lazy<T> lazy;

        public DbContextTest(string databaseName)
        {
            lazy = new Lazy<T>(() => DbContextConstructor(databaseName));
        }

        public T DbContext
        {
            get
            {
                return lazy.Value;
            }
        }

        public T DbContextConstructor(string databaseName)
        {
            var type = typeof(T);

            var dbOptionBuilder = new DbContextOptionsBuilder<T>();
            var inMemoryDbOptions = new InMemoryDbContextOptionsBuilder(dbOptionBuilder);
            dbOptionBuilder.UseInMemoryDatabase(databaseName);

            var dbContext = Activator.CreateInstance(type, dbOptionBuilder.Options);
            var typedDbContext = dbContext as T;

            typedDbContext.Seed();

            return typedDbContext;
        }

        ~DbContextTest()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}
