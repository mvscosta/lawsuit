using FluentValidation;
using FluentValidation.Results;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Update;
using System.Linq;

namespace Mc2Tech.PersonsApi.Validations
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator(ApiDbContext context)
        {
            RuleFor(p => p.Data.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(p => p.Data.Email)
                .EmailAddress()
                .MaximumLength(400);

            RuleFor(p => p.Data.Cpf)
                .IsValidCPF()
                .Custom((a, customContext) =>
                {
                    var dbset = context.Set<PersonEntity>();

                    if (dbset.Any(p => p.Cpf == a))
                    {
                        customContext.AddFailure(new ValidationFailure(customContext.PropertyName, $"{customContext.PropertyName} '{a}' already exists") { ErrorCode = "DuplicatedCpfValidator" });
                    }
                });
        }
    }
}

