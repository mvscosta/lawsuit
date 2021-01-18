using FluentValidation;
using FluentValidation.Results;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using System.Linq;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    public class CreateSituationCommandValidator : AbstractValidator<CreateSituationCommand>
    {
        public CreateSituationCommandValidator()
        {
            RuleFor(p => p.Data.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(p => p.Data.Description)
                .MaximumLength(1000);
        }
    }
}

