using FluentValidation;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.PersonsApi.ViewModel.Delete;

namespace Mc2Tech.PersonsApi.Validations.Delete
{
    public class SoftDeletePersonCommandValidator : AbstractValidator<SoftDeletePersonCommand>
    {
        public SoftDeletePersonCommandValidator()
        {
            RuleFor(p => p.Data.PersonId)
                .NotEmpty();
        }
    }
}

