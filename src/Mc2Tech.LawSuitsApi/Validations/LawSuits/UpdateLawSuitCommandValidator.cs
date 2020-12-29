using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    public class UpdateLawSuitCommandValidator : AbstractValidator<UpdateLawSuitCommand>
    {
        public UpdateLawSuitCommandValidator(ApiDbContext situationContext)
        {
            RuleFor(p => p.Data.SituationId)
                .NotEmpty()
                .Custom((a, customContext) =>
                {
                    var dbset = situationContext.Set<SituationEntity>();
                    var situation = dbset.First(p => p.Id == a);
                    if (situation.IsClosed)
                    {
                        customContext.AddFailure(
                            new ValidationFailure(
                                customContext.PropertyName,
                                $"Situation '{situation.Name}' not allow updates."
                            )
                            { ErrorCode = "SituationValidator" }
                        );
                    }
                });
            RuleFor(p => p.Data.ClientPhysicalFolder)
                .MaximumLength(50);
            RuleFor(p => p.Data.Description)
                .MaximumLength(1000);
        }
    }
}

