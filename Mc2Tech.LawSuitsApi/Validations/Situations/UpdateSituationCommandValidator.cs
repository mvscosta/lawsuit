using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Mc2Tech.LawSuitsApi.Validations.Situations
{
    public class UpdateSituationCommandValidator : AbstractValidator<UpdateSituationCommand>
    {
        public UpdateSituationCommandValidator()
        {
            RuleFor(p => p.Data.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(p => p.Data.Description)
                .MaximumLength(1000);
        }
    }
}

