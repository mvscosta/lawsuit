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
        /// <param name="situationDbContext"></param>
        public DeleteLawSuitCommandValidator(ApiDbContext apiDbContext, SituationDbContext situationDbContext)
        {

            RuleFor(p => p.Data.LawSuitId)
                .NotEmpty()
                .IsValidSituationStatus(apiDbContext, situationDbContext);
        }
    }
}

