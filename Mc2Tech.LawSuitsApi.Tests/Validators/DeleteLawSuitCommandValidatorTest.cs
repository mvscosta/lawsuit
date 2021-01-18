using AutoFixture;
using FluentAssertions;
using Mc2Tech.BaseTests;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Tests.Infrastructure;
using Mc2Tech.LawSuitsApi.Validations.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System;
using System.Linq;
using Xunit;

namespace Mc2Tech.LawSuitsApi.Tests.Validators
{
    public class DeleteLawSuitCommandValidatorTest
    {
        protected IFixture CreateFixture()
        {
            return new Fixture()
                .Customize(new AutoMoqComposite())
                .Customize(new IgnoreVirtualMembersCustomization());
        }

        protected IFixture CreateFixtureAllowVirtualMembers()
        {
            return new Fixture()
                .Customize(new AutoMoqComposite());
        }

        private ApiDbContext ApiDbContext;
        private SituationDbContext SituationDbContext;

        private DeleteLawSuitCommandValidator CreateValidator(string databaseName)
        {
            ApiDbContext = new DbContextTest<ApiDbContext>("ApiDb" + databaseName).DbContext;
            SituationDbContext = new DbContextTest<SituationDbContext>("SituationDb" + databaseName).DbContext;

            return new DeleteLawSuitCommandValidator(ApiDbContext, SituationDbContext);
        }

        [Fact]
        public void DeleteLawSuit_WithoutLawSuitId_ValidationError()
        {
            var validator = CreateValidator("DeleteLawSuit_WithoutLawSuitId_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<DeleteLawSuitModel>()
                .Without(p => p.LawSuitId)
                .Create();

            var command = fixture.Build<DeleteLawSuitCommand>()
                .With(p => p.Data, model)
                .Create();


            var result = validator.Validate(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.LawSuitId");
        }

        [Fact]
        public void DeleteLawSuit_ClosedSituationId_ValidationError()
        {
            var validator = CreateValidator("DeleteLawSuit_ClosedSituationId_ValidationError");

            var fixture = CreateFixture();

            var dbsetSituations = SituationDbContext.Set<SituationEntity>();
            var situationId = dbsetSituations.Where(s => s.IsClosed).Select(p => p.Id).First();

            var lawSuit = new LawSuitEntity
            {
                SituationId = situationId.Value,
                DistributedDate = DateTime.Now                
            };

            var dbsetLawSuits = ApiDbContext.Set<LawSuitEntity>();
            dbsetLawSuits.Add(lawSuit);
            ApiDbContext.SaveChanges();

            var model = fixture.Build<DeleteLawSuitModel>()
                .With(p => p.LawSuitId, lawSuit.Id)
                .Create();

            var command = fixture.Build<DeleteLawSuitCommand>()
                .With(p => p.Data, model)
                .Create();

            var result = validator.Validate(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.LawSuitId" && p.ErrorCode == "SituationClosedValidator");
        }
    }
}
