using AutoFixture;
using FluentAssertions;
using Mc2Tech.BaseTests;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.Tests.Infrastructure;
using Mc2Tech.PersonsApi.Validations.Create;
using Mc2Tech.PersonsApi.ViewModel.Create;
using Xunit;

namespace Mc2Tech.PersonsApi.Tests.Validators
{
    public class CreatePersonCommandValidatorTest
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

        private CreatePersonCommandValidator CreateValidator(string databaseName)
        {
            ApiDbContext = new DbContextTest<ApiDbContext>("ApiDb" + databaseName).DbContext;

            return new CreatePersonCommandValidator(ApiDbContext);
        }

        [Fact]
        public void CreatePerson_WithoutName_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_WithoutName_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .Without(p => p.Name)
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Name" && p.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void CreatePerson_NameBiggerThan150Chars_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_NameBiggerThan150Chars_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .With(p => p.Name, string.Join(string.Empty, fixture.CreateMany<string>(15)).Substring(0, 151))
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Name" && p.ErrorCode == "MaximumLengthValidator");
        }

        [Fact]
        public void CreatePerson_WithoutCpf_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_WithoutCpf_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .Without(p => p.Cpf)
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Cpf" && p.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void CreatePerson_CpfNotValid_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_CpfNotValid_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .With(p => p.Cpf, string.Join(string.Empty, fixture.CreateMany<string>(5)).Substring(0, 20))
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Cpf" && p.ErrorCode == "CPFValidator");
        }

        [Fact]
        public void CreatePerson_WithoutEmail_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_WithoutEmail_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .Without(p => p.Email)
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Email" && p.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void CreatePerson_EmailNotValid_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_EmailNotValid_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .With(p => p.Email, string.Join(string.Empty, fixture.CreateMany<string>(5)).Substring(0, 20))
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Email" && p.ErrorCode == "AspNetCoreCompatibleEmailValidator");
        }

        [Fact]
        public void CreatePerson_EmailBiggerThan400Chars_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_EmailNotValid_ValidationError");

            var fixture = CreateFixture();

            var model = fixture.Build<CreatePersonModel>()
                .With(p => p.Email, string.Join(string.Empty, fixture.CreateMany<string>(15)).Substring(0, 401))
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Email" && p.ErrorCode == "MaximumLengthValidator");
        }

        [Fact]
        public void CreatePerson_DuplicatedCpf_ValidationError()
        {
            var validator = CreateValidator("CreatePerson_DuplicatedCpf_ValidationError");

            var fixture = CreateFixture();

            var dbset = ApiDbContext.Set<PersonEntity>();
            dbset.Add(new PersonEntity { Cpf = "1234", Status = Crosscutting.Enums.ObjectStatus.Active });
            ApiDbContext.SaveChanges();

            var model = fixture.Build<CreatePersonModel>()
                .With(p => p.Cpf, "1234")
                .Create();

            var command = fixture.Build<CreatePersonCommand>()
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
                .Contain(p => p.PropertyName == "Data.Cpf" && p.ErrorCode == "DuplicatedKeyValidator");
        }
    }
}
