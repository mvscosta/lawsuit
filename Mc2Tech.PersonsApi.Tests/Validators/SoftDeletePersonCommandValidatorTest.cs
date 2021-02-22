using AutoFixture;
using FluentAssertions;
using Mc2Tech.BaseTests;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Tests.Infrastructure;
using Mc2Tech.PersonsApi.Validations.Delete;
using Mc2Tech.PersonsApi.ViewModel.Delete;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Mc2Tech.PersonsApi.Tests.Validators
{
    public class SoftDeletePersonCommandValidatorTest
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

        private SoftDeletePersonCommandValidator CreateValidator(string databaseName, int countLawSuitReference)
        {
            ApiDbContext = new DbContextTest<ApiDbContext>("ApiDb" + databaseName).DbContext;

            return new SoftDeletePersonCommandValidator();
        }

        [Fact]
        public void SoftDeletePerson_WithoutPersonId_ValidationError()
        {
            var validator = CreateValidator("SoftDeletePerson_WithoutPersonId_ValidationError", 0);

            var fixture = CreateFixture();

            var model = fixture.Build<SoftDeletePersonModel>()
                .Without(p => p.PersonId)
                .Create();

            var command = fixture.Build<SoftDeletePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.PersonId");
        }
    }
}
