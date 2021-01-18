using FluentValidation;
using System.Linq;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Validator class to create law suilt command
    /// </summary>
    public class CreateLawSuitCommandValidator : AbstractValidator<CreateLawSuitCommand>
    {
        /// <summary>
        /// Constructor Business logic definition
        /// </summary>
        /// <param name="apiDbContext"></param>
        /// <param name="situationDbContext"></param>
        /// <param name="personsApiServiceClient"></param>
        public CreateLawSuitCommandValidator(ApiDbContext apiDbContext, SituationDbContext situationDbContext, IPersonsApiServiceClient personsApiServiceClient)
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
                .IsValidUnifiedProcessNumber()
                .IsUniqueUnifiedProcessNumberLawSuitValidator(apiDbContext);
            RuleFor(p => p.Data.DistributedDate)
                .NotEmpty()
                .LessThan(DateTime.UtcNow.Date.AddDays(1));
            RuleFor(p => p.Data.LawSuitResponsibles)
                .NotEmpty()
                .Must(p => p != null && !p.GroupBy(x => x).Any(g => g.Count() > 1))
                .WithMessage("Law Suit Responsible duplicated.");
            RuleFor(p => p.Data.LawSuitResponsibles)
                .Must(p => p != null && p.Count() <= 3)
                .WithMessage("Maximum 3 Law Suit Responsible.");

            RuleFor(p => p)
                .IsValidLawSuitResponsibles(apiDbContext, personsApiServiceClient);
        }
    }
}

