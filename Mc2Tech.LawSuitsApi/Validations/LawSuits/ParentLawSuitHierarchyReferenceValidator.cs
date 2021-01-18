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
    /// Includes validation if reference existing parent or child reference
    /// </summary>
    public class ParentLawSuitHierarchyReferenceValidator : PropertyValidator
    {
        private readonly ApiDbContext _apiDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiDbContext"></param>
        public ParentLawSuitHierarchyReferenceValidator(ApiDbContext apiDbContext)
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
                $"{context.PropertyName} '{value}' invalid parent law suit hierarchy references."               
            );

            failure.ErrorCode = "ParentLawSuitHierarchyReferenceValidator";

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

            var parent = await dbset.AnyAsync(p => p.Id == value.Value, ct);

            var parentAlreadyReferenced = await dbset
                .AnyAsync(p => p.ParentLawSuitId == value.Value, ct);

            //Parent law suit id informed needs to exist
            if (!parent)
            {
                return false;
            }

            //// only one reference in the hierarchy
            if (parentAlreadyReferenced)
            {
                return false;
            }

            return true;
        }
    }
}
