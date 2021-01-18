using FluentValidation;
using Mc2Tech.PersonsApi.Model;

namespace Mc2Tech.Validator.LawSuits
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(p => p.Email)
                .EmailAddress()
                .MaximumLength(400);

            RuleFor(p => p.Cpf)
                .NotEmpty()
                .IsValidCPF();
        }
    }
}
