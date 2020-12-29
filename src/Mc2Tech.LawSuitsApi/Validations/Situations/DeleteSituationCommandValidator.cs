using FluentValidation;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;

namespace Mc2Tech.LawSuitsApi.Validations.Situations
{
    public class DeleteSituationCommandValidator : AbstractValidator<DeleteSituationCommand>
    {
        public DeleteSituationCommandValidator()
        {
            RuleFor(p => p.Data.Id)
                .NotEmpty();
        }
    }
}

