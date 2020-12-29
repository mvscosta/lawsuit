using FluentValidation;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Law Suit Validator Extensions
    /// </summary>
    public static class SituationValidatorExtensions
    {
        /// <summary>
        /// Extension to validate if the situation exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="situationDbContext"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, Guid> IsValidSituation<T>(this IRuleBuilder<T, Guid> ruleBuilder, SituationDbContext situationDbContext)
        {
            return ruleBuilder.SetValidator(new SituationValidator(situationDbContext));
        }
    }
}
