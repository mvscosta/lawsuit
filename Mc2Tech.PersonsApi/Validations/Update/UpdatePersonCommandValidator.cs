using FluentValidation;
using Mc2Tech.PersonsApi.ViewModel.Update;

namespace Mc2Tech.PersonsApi.Validations.Update
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(p => p.Data.Id)
                .NotEmpty();

            RuleFor(p => p.Data.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(p => p.Data.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(400);

        }
    }
}

