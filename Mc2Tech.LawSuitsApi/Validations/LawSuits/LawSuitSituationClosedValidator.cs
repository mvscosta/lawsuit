using FluentValidation.Results;
using FluentValidation.Validators;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Class to validate the situation
    /// </summary>
    public class LawSuitSituationClosedValidator : PropertyValidator
    {
        private readonly SituationDbContext _situationDbContext;
        private readonly ApiDbContext _apiDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiDbContext"></param>
        /// <param name="situationDbContext"></param>
        public LawSuitSituationClosedValidator(ApiDbContext apiDbContext, SituationDbContext situationDbContext)
        {
            this._apiDbContext = apiDbContext;
            this._situationDbContext = situationDbContext;
        }

        /// <summary>
        /// Sync is valid method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            return IsValidAsync(context, new CancellationToken()).Result;
        }

        /// <summary>
        /// Costomize error message
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override ValidationFailure CreateValidationError(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as Guid?;

            var failure = new ValidationFailure(
                context.PropertyName,
                $"{context.PropertyName} '{value}' invalid situation, status is closed."
            );

            failure.ErrorCode = "SituationClosedValidator";
            
            return failure;
        }

        /// <summary>
        /// Async method to validate property
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async override Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken ct)
        {
            var lawSuitValue = context.PropertyValue as Guid?;

            if (!lawSuitValue.HasValue)
            {
                return false;
            }

            var dbsetLawSuit = _apiDbContext.Set<LawSuitEntity>();
            var situationId = dbsetLawSuit
                .Where(p => p.Id == lawSuitValue)
                .Select(p => p.SituationId)
                .FirstOrDefault();

            var dbsetSituation = _situationDbContext.Set<SituationEntity>();

            var notClosed = await dbsetSituation
                .AnyAsync(p => 
                    p.Id == situationId
                    && p.IsClosed == false
                , ct);

            return notClosed;
        }
    }
}
