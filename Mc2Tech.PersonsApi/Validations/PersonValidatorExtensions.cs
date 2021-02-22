using FluentValidation;
using Mc2Tech.PersonsApi.DAL;

namespace Mc2Tech.PersonsApi.Validations
{
    /// <summary>
    /// Person Validator Extensions
    /// </summary>
    public static class PersonValidatorExtensions
    {
        /// <summary>
        /// Extension to validate if the CPF is unique
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="apiDbContext"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsUniqueCpfPersonValidator<T>(this IRuleBuilder<T, string> ruleBuilder, ApiDbContext apiDbContext)
        {
            return ruleBuilder.SetValidator(new UniqueCpfPersonValidator(apiDbContext));
        }
    }
}
