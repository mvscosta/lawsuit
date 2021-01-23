using FluentValidation.Results;
using FluentValidation.Validators;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Validations
{
    /// <summary>
    /// Class extension to unique CPF validation
    /// </summary>
    public class UniqueCpfPersonValidator : PropertyValidator
    {
        private readonly ApiDbContext _apiDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiDbContext"></param>
        public UniqueCpfPersonValidator(ApiDbContext apiDbContext)
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
            var value = context.PropertyValue as string ?? string.Empty;

            var failure = new ValidationFailure(
                context.PropertyName,
                $"{context.PropertyName} '{value}' already exists"
            );

            failure.ErrorCode = "DuplicatedKeyValidator";

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
            var value = context.PropertyValue as string ?? string.Empty;

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            var dbset = _apiDbContext.Set<PersonEntity>();

            if (await dbset.AnyAsync(p => p.Cpf == value, ct))
            {
                return false;
            }

            return true;
        }
    }
}
