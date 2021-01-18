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
    public class SituationValidator : PropertyValidator
    {
        private readonly SituationDbContext _situationDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="situationDbContext"></param>
        public SituationValidator(SituationDbContext situationDbContext)
        {
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
                $"{context.PropertyName} '{value}' invalid situation, not found."
            );

            failure.ErrorCode = "SituationValidator";
            
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
            var value = context.PropertyValue as Guid?;

            if (!value.HasValue)
            {
                return true;
            }

            var dbset = _situationDbContext.Set<SituationEntity>();

            var exists = await dbset
                .AnyAsync(p => 
                    p.Id == value.Value 
                , ct);

            return exists;
        }
    }
}
