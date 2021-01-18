using AutoFixture;
using FluentAssertions;
using Mc2Tech.BaseTests;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.Crosscutting.ViewModel.LawSuits;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Tests.Infrastructure;
using Mc2Tech.LawSuitsApi.Validations.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2Tech.LawSuitsApi.Tests.Validators
{
    public class UpdateLawSuitCommandValidatorTest
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

        private UpdateLawSuitCommandValidator CreateValidator(string databaseName)
        {
            ApiDbContext = new DbContextTest<ApiDbContext>("ApiDb" + databaseName).DbContext;
            SituationDbContext = new DbContextTest<SituationDbContext>("SituationDb" + databaseName).DbContext;

            var personsApiServiceClient = new Mock<IPersonsApiServiceClient>();
            personsApiServiceClient
                .Setup(exp => exp.GetPersonIdExistsAsync(It.IsAny<HttpRequestPayloadDto>(), Guid.Empty, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            return new UpdateLawSuitCommandValidator(ApiDbContext, SituationDbContext, personsApiServiceClient.Object);
        }

        [Fact]
        public void UpdateLawSuit_WithoutLawSuitId_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_WithoutLawSuitId_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<UpdateLawSuitModel>()
                .Without(p => p.Id)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.Id");
        }

        [Fact]
        public void UpdateLawSuit_ClosedSituationId_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_ClosedSituationId_ValidationError");

            var fixture = CreateFixture();

            var dbset = SituationDbContext.Set<SituationEntity>();

            var situationId = dbset.Where(s => s.IsClosed).Select(p => p.Id).First();

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.SituationId, situationId)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.Id" && p.ErrorCode == "SituationClosedValidator");
        }

        [Fact]
        public void UpdateLawSuit_DistributedDateBiggerThanToday_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_DistributedDateBiggerThanToday_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.DistributedDate, DateTime.UtcNow.AddDays(1))
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.DistributedDate" && p.ErrorCode == "LessThanValidator");
        }

        [Fact]
        public async Task UpdateLawSuit_DistributedDateTomorrow_DistributedDateError()
        {
            var validator = CreateValidator("UpdateLawSuit_DistributedDateTomorrow_DistributedDateError");

            var fixture = CreateFixture();

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.DistributedDate, DateTime.UtcNow.Date.AddDays(1))
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
                .With(p => p.Data, model)
                .Create();

            var result = await validator.ValidateAsync(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.DistributedDate" && p.ErrorCode == "LessThanValidator");
        }

        [Fact]
        public void UpdateLawSuit_DistributedDateToday_NoDistributedDateError()
        {
            var validator = CreateValidator("UpdateLawSuit_DistributedDateToday_NoDistributedDateError");

            var fixture = CreateFixture();

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.DistributedDate, DateTime.UtcNow.Date.AddDays(1).AddMilliseconds(-1))
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
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
                .NotContain(p => p.PropertyName == "Data.DistributedDate");
        }

        [Fact]
        public async Task UpdateLawSuit_ClientPhysicalFolderBiggerThan50Chars_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_ClientPhysicalFolderBiggerThan50Chars_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.ClientPhysicalFolder, string.Join(string.Empty, fixture.CreateMany<string>(5)).Substring(0, 51))
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
                .With(p => p.Data, model)
                .Create();


            var result = await validator.ValidateAsync(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.ClientPhysicalFolder" && p.ErrorCode == "MaximumLengthValidator");
        }

        [Fact]
        public async Task UpdateLawSuit_DescriptionBiggerThan1000Chars_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_DescriptionBiggerThan1000Chars_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.Description, string.Join(string.Empty, fixture.CreateMany<string>(39)).Substring(0, 1001))
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
                .With(p => p.Data, model)
                .Create();


            var result = await validator.ValidateAsync(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.Description" && p.ErrorCode == "MaximumLengthValidator");
        }

        [Fact]
        public void UpdateLawSuit_WithDuplicatedResponsible_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_WithDuplicatedResponsible_ValidationError");

            var fixture = CreateFixture();

            var responsibles = fixture.Build<LawSuitResponsibleModel>()
                .With(p => p.PersonId, Guid.Empty)
                .CreateMany(2);

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.LawSuitResponsibles, responsibles)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.LawSuitResponsibles" && p.ErrorMessage == "Law Suit Responsible duplicated.");
        }

        [Fact]
        public void UpdateLawSuit_WithMoreThan3Responsible_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_WithMoreThan3Responsible_ValidationError");

            var fixture = CreateFixture();

            var responsibles = fixture.Build<LawSuitResponsibleModel>()
                .CreateMany(4);

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.LawSuitResponsibles, responsibles)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.LawSuitResponsibles" && p.ErrorMessage == "Maximum 3 Law Suit Responsible.");
        }

        [Fact]
        public async Task UpdateLawSuit_ExistingResponsible_NoResponsibleValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_ExistingResponsible_NoResponsibleValidationError");

            var fixture = CreateFixture();

            var lawSuitId = Guid.NewGuid();

            var responsibles = fixture.Build<LawSuitResponsibleModel>()
                .With(p => p.PersonId, Guid.Empty)
                .With(p => p.LawSuitId, lawSuitId)
                .CreateMany(1);

            var model = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.Id, lawSuitId)
                .With(p => p.LawSuitResponsibles, responsibles)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
                .With(p => p.Data, model)
                .Create();


            var result = await validator.ValidateAsync(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .NotContain(p => p.PropertyName == "Data.LawSuitResponsibles");
        }

        [Fact]
        public async Task UpdateLawSuit_WrongReferenceHierarchy_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_WrongReferenceHierarchy_ValidationError");

            var fixture = CreateFixture();

            var dbset = ApiDbContext.Set<LawSuitEntity>();

            var modelL1 = fixture.Build<LawSuitEntity>()
                .Without(p => p.ParentLawSuitId)
                .Create();

            await dbset.AddAsync(modelL1);

            var modelL2 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL1.Id)
                .Create();

            await dbset.AddAsync(modelL2);

            var modelL3 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL2.Id)
                .Create();

            await dbset.AddAsync(modelL3);
            await ApiDbContext.SaveChangesAsync();

            var modelL4 = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.ParentLawSuitId, modelL2.Id)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
                .With(p => p.Data, modelL4)
                .Create();


            var result = await validator.ValidateAsync(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.ParentLawSuitId" && p.ErrorCode == "ParentLawSuitHierarchyReferenceValidator");
        }

        [Fact]
        public async Task UpdateLawSuit_HierararchyBiggerThan4_ValidationError()
        {
            var validator = CreateValidator("UpdateLawSuit_HierararchyBiggerThan4_ValidationError");

            var fixture = CreateFixture();

            var dbset = ApiDbContext.Set<LawSuitEntity>();

            var modelL1 = fixture.Build<LawSuitEntity>()
                .Without(p => p.ParentLawSuitId)
                .Create();

            await dbset.AddAsync(modelL1);

            var modelL2 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL1.Id)
                .Create();

            await dbset.AddAsync(modelL2);

            var modelL3 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL2.Id)
                .Create();

            await dbset.AddAsync(modelL3);

            var modelL4 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL3.Id)
                .Create();

            await dbset.AddAsync(modelL4);
            await ApiDbContext.SaveChangesAsync();

            var modelL5 = fixture.Build<UpdateLawSuitModel>()
                .With(p => p.ParentLawSuitId, modelL4.Id)
                .Create();

            var command = fixture.Build<UpdateLawSuitCommand>()
                .With(p => p.Data, modelL5)
                .Create();


            var result = validator.Validate(command);

            result
                .Errors
                .Should()
                .NotBeNull();

            result
                .Errors
                .Should()
                .Contain(p => p.PropertyName == "Data.ParentLawSuitId" && p.ErrorCode == "ParentLawSuitHierarchySizeValidator");
        }
    }
}
