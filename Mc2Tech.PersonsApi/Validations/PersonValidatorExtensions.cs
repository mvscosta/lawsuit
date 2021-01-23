using FluentValidation;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.ViewModel.Delete;

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

        /// <summary>
        /// Extension to validate if person is not referenced in lawsuits
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="lawSuitsApiServiceClient"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, DeletePersonCommand> IsLawSuitReferencedPersonValidator<T>(this IRuleBuilder<T, DeletePersonCommand> ruleBuilder, ILawSuitsApiServiceClient lawSuitsApiServiceClient)
        {
            return ruleBuilder.SetValidator(new LawSuitReferencedValidator(lawSuitsApiServiceClient));
        }
    }
}
