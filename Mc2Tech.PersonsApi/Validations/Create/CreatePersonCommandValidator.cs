using FluentValidation;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.ViewModel.Create;

namespace Mc2Tech.PersonsApi.Validations.Create
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator(ApiDbContext apiDbContext)
        {
            RuleFor(p => p.Data.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(p => p.Data.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(400);

            RuleFor(p => p.Data.Cpf)
                .NotEmpty()
                .IsValidCPF()
                .IsUniqueCpfPersonValidator(apiDbContext);
        }
    }
}

