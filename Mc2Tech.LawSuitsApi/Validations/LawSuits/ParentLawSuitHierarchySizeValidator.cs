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
    /// Class to validate if the parent law suit informed is valid
    /// Includes validation if overflow the limit of 3 in depth references
    /// </summary>
    public class ParentLawSuitHierarchySizeValidator : PropertyValidator
    {
        private readonly ApiDbContext _apiDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiDbContext"></param>
        public ParentLawSuitHierarchySizeValidator(ApiDbContext apiDbContext)
        {
            this._apiDbContext = apiDbContext;
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
                $"{context.PropertyName} '{value}' invalid parent law suit hierarchy size, limit 4."               
            );

            failure.ErrorCode = "ParentLawSuitHierarchySizeValidator";

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
            var dbset = _apiDbContext.Set<LawSuitEntity>();

            var parent = await dbset
                .Include(p => p.ParentLawSuit.ParentLawSuit.ParentLawSuit)
                .Where(p => p.Id == value.Value)
                .Select(p => new LawSuitEntity
                {
                    Id = p.Id,
                    ParentLawSuitId = p.ParentLawSuitId,
                    ParentLawSuit = new LawSuitEntity
                    {
                        Id = p.ParentLawSuit.Id,
                        ParentLawSuitId = p.ParentLawSuit.ParentLawSuitId,

                        ParentLawSuit = new LawSuitEntity
                        {
                            Id = p.ParentLawSuit.ParentLawSuit.Id,
                            ParentLawSuitId = p.ParentLawSuit.ParentLawSuit.ParentLawSuitId,
                        }
                    }
                })
                .FirstOrDefaultAsync(ct);

            //Parent law suit id informed needs to exist
            if (parent == null)
            {
                return false;
            }

            // max depth of 4 process in hierarchy
            if (parent.ParentLawSuit.ParentLawSuit.ParentLawSuitId != null)
            {
                return false;
            }

            //// only one reference in the hierarchy
            //if (parent.Id == parent.ParentLawSuitId || parent.Id == parent.ParentLawSuit.ParentLawSuitId)
            //{
            //    return false;
            //}

            return true;
        }
    }
}
