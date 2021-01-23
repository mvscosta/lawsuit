using FluentValidation;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.PersonsApi.ViewModel.Delete;

namespace Mc2Tech.PersonsApi.Validations.Delete
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator(ILawSuitsApiServiceClient lawSuitsApiServiceClient)
        {
            RuleFor(p => p.Data.PersonId)
                .NotEmpty();

            RuleFor(p => p)
                .IsLawSuitReferencedPersonValidator(lawSuitsApiServiceClient);
        }
    }
}

