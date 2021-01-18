using FluentValidation;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System;
using System.Linq;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    public class UpdateLawSuitCommandValidator : AbstractValidator<UpdateLawSuitCommand>
    {
        public UpdateLawSuitCommandValidator(ApiDbContext apiDbContext, SituationDbContext situationDbContext, IPersonsApiServiceClient personsApiServiceClient)
        {
            RuleFor(p => p.Data.ParentLawSuitId)
                .IsValidParentLawSuit(apiDbContext);

            RuleFor(p => p.Data.Id)
                .NotEmpty()
                .IsValidSituationStatus(apiDbContext, situationDbContext);

            RuleFor(p => p.Data.SituationId)
                .IsValidSituation(situationDbContext);
            RuleFor(p => p.Data.ClientPhysicalFolder)
                .MaximumLength(50);
            RuleFor(p => p.Data.Description)
                .MaximumLength(1000);
            RuleFor(p => p.Data.DistributedDate)
                .LessThan(DateTime.UtcNow.Date.AddDays(1));

            RuleFor(p => p.Data.LawSuitResponsibles)
                .Must(p => p == null || !p.GroupBy(x => x.PersonId).Any(g => g.Count() > 1))
                .WithMessage("Law Suit Responsible duplicated.")
                .WithErrorCode("LawSuitResponsiblesDuplicatedValidator");
            RuleFor(p => p.Data.LawSuitResponsibles)
                .Must(p => p == null || p.Count() <= 3)
                .WithMessage("Maximum 3 Law Suit Responsible.")
                .WithErrorCode("LawSuitResponsiblesMaximumSizeValidator");
            RuleFor(p => p.Data.LawSuitResponsibles)
                .Must(p => p == null || p.All(p => p.LawSuitId != Guid.Empty))
                .WithMessage("LawSuitId in Responsibles is required.")
                .WithErrorCode("LawSuitResponsiblesRequiredValidator");
            RuleFor(p => p)
                .IsValidLawSuitResponsibles(apiDbContext, personsApiServiceClient);

        }
    }
}

