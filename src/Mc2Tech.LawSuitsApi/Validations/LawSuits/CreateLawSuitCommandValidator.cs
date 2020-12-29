using FluentValidation;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Validator class to create law suilt command
    /// </summary>
    public class CreateLawSuitCommandValidator : AbstractValidator<CreateLawSuitCommand>
    {
        /// <summary>
        /// Constructor logic definition
        /// </summary>
        /// <param name="apiDbContext"></param>
        /// <param name="situationDbContext"></param>
        public CreateLawSuitCommandValidator(ApiDbContext apiDbContext, SituationDbContext situationDbContext)
        {
            RuleFor(p => p.Data.ParentLawSuitId)
                .IsValidParentLawSuit(apiDbContext);

            RuleFor(p => p.Data.SituationId)
                .NotEmpty()
                .IsValidSituation(situationDbContext);
            RuleFor(p => p.Data.JusticeSecret)
                .NotNull();
            RuleFor(p => p.Data.ClientPhysicalFolder)
                .MaximumLength(50);
            RuleFor(p => p.Data.Description)
                .MaximumLength(1000);
            RuleFor(p => p.Data.UnifiedProcessNumber)
                .NotEmpty()
                .Length(20)
                .IsUniqueUnifiedProcessNumberLawSuitValidator(apiDbContext);
        }
    }
}

