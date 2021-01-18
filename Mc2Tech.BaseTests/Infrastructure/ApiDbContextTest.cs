//using Mc2Tech.LawSuitsApi.DAL;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using System;

//namespace Mc2Tech.LawSuitsService.Tests
//{
//    public sealed class ApiDbContextTest
//    {
//        private static readonly object padlock = new object();

//        private static readonly Lazy<ApiDbContext> lazy = new Lazy<ApiDbContext>(() => ApiDbContextConstructor());
//        public static ApiDbContext VisitorsContext
//        {
//            get
//            {
//                return lazy.Value;
//            }
//        }

//        public static ApiDbContext ApiDbContextConstructor()
//        {
//            var dbOptionBuilder = new DbContextOptionsBuilder<ApiDbContext>();
//            var inMemoryDbOptions = new InMemoryDbContextOptionsBuilder(dbOptionBuilder);
//            dbOptionBuilder.UseInMemoryDatabase("VisitorsContext");

//            var visitorsContext = new ApiDbContext(dbOptionBuilder.Options);

//            visitorsContext.Seed();

//            return visitorsContext;
//        }
//    }
//}
