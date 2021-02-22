using AutoFixture;
using FluentAssertions;
using Mc2Tech.BaseTests;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Tests.Infrastructure;
using Mc2Tech.LawSuitsApi.Validations.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Mc2Tech.LawSuitsApi.Tests.Validators
{
    public class CreateLawSuitCommandValidatorTest
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

        private CreateLawSuitCommandValidator CreateValidator(string databaseName)
        {
            ApiDbContext = new DbContextTest<ApiDbContext>("ApiDb" + databaseName).DbContext;
            SituationDbContext = new DbContextTest<SituationDbContext>("SituationDb" + databaseName).DbContext;

            var personsApiServiceClient = new Mock<IPersonsApiServiceClient>();
            personsApiServiceClient.Setup(exp => exp.GetPersonIdExistsAsync(It.IsAny<HttpRequestPayloadDto>(), Guid.Empty, It.IsAny<CancellationToken>()));

            return new CreateLawSuitCommandValidator(ApiDbContext, SituationDbContext, personsApiServiceClient.Object);
        }

        [Fact]
        public void CreateLawSuit_WithoutUnifiedProcessNumber_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithoutUnifiedProcessNumber_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .Without(p => p.UnifiedProcessNumber)
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.UnifiedProcessNumber" && p.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void CreateLawSuit_WithUnifiedProcessNumberDiff20Chars_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithUnifiedProcessNumberDiff20Chars_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.UnifiedProcessNumber, "1234")
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.UnifiedProcessNumber" && p.ErrorCode == "ExactLengthValidator");
        }

        [Fact]
        public void CreateLawSuit_DuplicatedUnifiedProcessNumber_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_DuplicatedUnifiedProcessNumber_ValidationError");

            var fixture = CreateFixture();

            var dbset = ApiDbContext.Set<LawSuitEntity>();
            dbset.Add(new LawSuitEntity { UnifiedProcessNumber = "1234", Status = Crosscutting.Enums.ObjectStatus.Active });
            ApiDbContext.SaveChanges();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.UnifiedProcessNumber, "1234")
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.UnifiedProcessNumber" && p.ErrorCode == "DuplicatedKeyValidator");
        }

        [Fact]
        public void CreateLawSuit_WithUnifiedProcessNumberWrongDigit_NoUnifiedProcessNumberValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithUnifiedProcessNumberWrongDigit_NoUnifiedProcessNumberValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.UnifiedProcessNumber, "00989283120098240000")
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .NotContain(p => p.PropertyName == "Data.UnifiedProcessNumber");
        }

        [Fact]
        public void CreateLawSuit_DistributedDateBiggerThanToday_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_DistributedDateBiggerThanToday_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.DistributedDate, DateTime.UtcNow.AddDays(1))
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
        public void CreateLawSuit_DistributedDateTomorrow_DistributedDateError()
        {
            var validator = CreateValidator("CreateLawSuit_DistributedDateTomorrow_DistributedDateError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.DistributedDate, DateTime.UtcNow.Date.AddDays(1))
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
        public void CreateLawSuit_DistributedDateToday_NoDistributedDateError()
        {
            var validator = CreateValidator("CreateLawSuit_DistributedDateToday_NoDistributedDateError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.DistributedDate, DateTime.UtcNow.Date.AddDays(1).AddMilliseconds(-1))
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
        public void CreateLawSuit_ClientPhysicalFolderBiggerThan50Chars_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_ClientPhysicalFolderBiggerThan50Chars_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.ClientPhysicalFolder, string.Join(string.Empty, fixture.CreateMany<string>(5)).Substring(0, 51))
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.ClientPhysicalFolder" && p.ErrorCode == "MaximumLengthValidator");
        }

        [Fact]
        public void CreateLawSuit_DescriptionBiggerThan1000Chars_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_DescriptionBiggerThan1000Chars_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.Description, string.Join(string.Empty, fixture.CreateMany<string>(39)).Substring(0, 1001))
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.Description" && p.ErrorCode == "MaximumLengthValidator");
        }

        [Fact]
        public void CreateLawSuit_WithoutSituation_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithoutSituation_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .Without(p => p.SituationId)
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.SituationId" && p.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void CreateLawSuit_WithoutResponsible_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithoutResponsible_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .Without(p => p.LawSuitResponsibles)
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .Contain(p => p.PropertyName == "Data.LawSuitResponsibles" && p.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void CreateLawSuit_WithDuplicatedResponsible_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithDuplicatedResponsible_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.LawSuitResponsibles, new List<Guid> { Guid.Empty, Guid.Empty })
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
        public void CreateLawSuit_WithMoreThan3Responsible_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WithMoreThan3Responsible_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.LawSuitResponsibles, new List<Guid> { Guid.Empty, Guid.Empty, Guid.Empty , Guid.Empty })
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
        public void CreateLawSuit_ExistingResponsible_NoResponsibleValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_ExistingResponsible_NoResponsibleValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreateLawSuitModel>()
                .With(p => p.LawSuitResponsibles, new List<Guid> { Guid.Empty })
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
                .NotContain(p => p.PropertyName == "Data.LawSuitResponsibles");
        }

        [Fact]
        public void CreateLawSuit_WrongReferenceHierarchy_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_WrongReferenceHierarchy_ValidationError");

            var fixture = CreateFixture();

            var dbset = ApiDbContext.Set<LawSuitEntity>();

            var modelL1 = fixture.Build<LawSuitEntity>()
                .Without(p => p.ParentLawSuitId)
                .Create();

            dbset.Add(modelL1);
            ApiDbContext.SaveChanges();

            var modelL2 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL1.Id)
                .Create();

            dbset.Add(modelL2);
            ApiDbContext.SaveChanges();

            var modelL3 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL2.Id)
                .Create();

            dbset.Add(modelL3);
            ApiDbContext.SaveChanges();

            var modelL4 = fixture.Build<CreateLawSuitModel>()
                .With(p => p.ParentLawSuitId, modelL2.Id)
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
                .With(p => p.Data, modelL4)
                .Create();


            var result = validator.Validate(command);

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
        public void CreateLawSuit_HierararchyBiggerThan4_ValidationError()
        {
            var validator = CreateValidator("CreateLawSuit_HierararchyBiggerThan4_ValidationError");

            var fixture = CreateFixture();

            var dbset = ApiDbContext.Set<LawSuitEntity>();

            var modelL1 = fixture.Build<LawSuitEntity>()
                .Without(p => p.ParentLawSuitId)
                .Create();

            dbset.Add(modelL1);

            var modelL2 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL1.Id)
                .Create();

            dbset.Add(modelL2);

            var modelL3 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL2.Id)
                .Create();

            dbset.Add(modelL3);

            var modelL4 = fixture.Build<LawSuitEntity>()
                .With(p => p.ParentLawSuitId, modelL3.Id)
                .Create();

            dbset.Add(modelL4);
            ApiDbContext.SaveChanges();

            var modelL5 = fixture.Build<CreateLawSuitModel>()
                .With(p => p.ParentLawSuitId, modelL4.Id)
                .Create();

            var command = fixture.Build<CreateLawSuitCommand>()
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
