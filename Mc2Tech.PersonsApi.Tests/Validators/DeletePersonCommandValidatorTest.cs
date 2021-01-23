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
    public class DeletePersonCommandValidatorTest
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

        private DeletePersonCommandValidator CreateValidator(string databaseName, int countLawSuitReference)
        {
            ApiDbContext = new DbContextTest<ApiDbContext>("ApiDb" + databaseName).DbContext;

            var lawSuitsApiServiceClient = new Mock<ILawSuitsApiServiceClient>();
            lawSuitsApiServiceClient
                .Setup(exp => exp.GetCountByResponsibleIdAsync(It.IsAny<HttpRequestPayloadDto>(), Guid.Empty, It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            return new DeletePersonCommandValidator(lawSuitsApiServiceClient.Object);
        }

        [Fact]
        public void DeletePerson_WithoutPersonId_ValidationError()
        {
            var validator = CreateValidator("DeletePerson_WithoutPersonId_ValidationError", 0);

            var fixture = CreateFixture();

            var model = fixture.Build<DeletePersonModel>()
                .Without(p => p.PersonId)
                .Create();

            var command = fixture.Build<DeletePersonCommand>()
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

        [Fact]
        public void DeletePerson_LawSuitReferenced_ValidationError()
        {
            var validator = CreateValidator("DeletePerson_LawSuitReferenced_ValidationError", 1);

            var fixture = CreateFixture();

            var model = fixture.Build<DeletePersonModel>()
                .With(p => p.PersonId, Guid.Empty)
                .Create();

            var command = fixture.Build<DeletePersonCommand>()
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
                .Contain(p => p.PropertyName == "DeletePersonModel" && p.ErrorCode == "LawSuitsReferencedValidator");
        }
    }
}
