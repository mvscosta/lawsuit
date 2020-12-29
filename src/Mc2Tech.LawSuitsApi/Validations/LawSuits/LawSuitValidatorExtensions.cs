using FluentValidation;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Law Suit Validator Extensions
    /// </summary>
    public static class LawSuitValidatorExtensions
    {
        /// <summary>
        /// Extension to validate if the parent law suit informed is valid
        /// Includes validation if overflow the limit of 3 in depth references
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="apiDbContext"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, Guid?> IsValidParentLawSuit<T>(this IRuleBuilder<T, Guid?> ruleBuilder, ApiDbContext apiDbContext)
        {
            return ruleBuilder.SetValidator(new ParentLawSuitValidator(apiDbContext));
        }

        /// <summary>
        /// Extension to validate if the unified process number is unique
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="apiDbContext"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsUniqueUnifiedProcessNumberLawSuitValidator<T>(this IRuleBuilder<T, string> ruleBuilder, ApiDbContext apiDbContext)
        {
            return ruleBuilder.SetValidator(new UniqueUnifiedProcessNumberLawSuitValidator(apiDbContext));
        }
    }
}
