using FluentValidation;
using FluentValidation.Results;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System.Linq;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Delete command for lawsuit validator
    /// </summary>
    public class DeleteLawSuitCommandValidator : AbstractValidator<DeleteLawSuitCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lawSuitContext"></param>
        /// <param name="situationContext"></param>
        public DeleteLawSuitCommandValidator(ApiDbContext lawSuitContext, ApiDbContext situationContext)
        {
            RuleFor(p => p.Data.Id)
                .NotEmpty()
            .Custom((a, customContext) =>
                {
                    var lawSuitDbSet = lawSuitContext.Set<LawSuitEntity>();
                    var lawSuit = lawSuitDbSet.First(p => p.Id == a);

                    var situationDbSet = situationContext.Set<SituationEntity>();
                    var situation = situationDbSet.First(p => p.Id == lawSuit.SituationId);

                    if (situation.IsClosed)
                    {
                        customContext.AddFailure(
                            new ValidationFailure(
                                customContext.PropertyName,
                                $"Situation '{situation.Name}' not allow deletion."
                            )
                            { ErrorCode = "SituationValidator" }
                        );
                    }
                });
        }
    }
}

