using FluentValidation;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
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
            return ruleBuilder.SetValidator(new ParentLawSuitHierarchySizeValidator(apiDbContext))
                .SetValidator(new ParentLawSuitHierarchyReferenceValidator(apiDbContext));
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

        /// <summary>
        /// Validate if all responsibles ids is valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="apiDbContext"></param>
        /// <param name="personsApiServiceClient"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, CreateLawSuitCommand> IsValidLawSuitResponsibles<T>(this IRuleBuilder<T, CreateLawSuitCommand> ruleBuilder, ApiDbContext apiDbContext, IPersonsApiServiceClient personsApiServiceClient)
        {
            return ruleBuilder.SetValidator(new LawSuitResponsibleIdsValidator(apiDbContext, personsApiServiceClient));
        }

        /// <summary>
        /// Validate if all responsibles is valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="apiDbContext"></param>
        /// <param name="personsApiServiceClient"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, UpdateLawSuitCommand> IsValidLawSuitResponsibles<T>(this IRuleBuilder<T, UpdateLawSuitCommand> ruleBuilder, ApiDbContext apiDbContext, IPersonsApiServiceClient personsApiServiceClient)
        {
            return ruleBuilder.SetValidator(new LawSuitResponsiblesValidator(apiDbContext, personsApiServiceClient));
        }

        /// <summary>
        /// Validate unified process number is valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsValidUnifiedProcessNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new LawSuitUnifiedProcessNumberValidator());
        }

        /// <summary>
        /// Validator to check if Situation status is not closed
        /// this validation is used in edit and delete commands
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="apiDbContext"></param>
        /// <param name="situationDbContext"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, Guid> IsValidSituationStatus<T>(this IRuleBuilder<T, Guid> ruleBuilder, ApiDbContext apiDbContext, SituationDbContext situationDbContext)
        {
            return ruleBuilder.SetValidator(new LawSuitSituationClosedValidator(apiDbContext, situationDbContext));
        }
    }
}
