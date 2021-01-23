using FluentValidation.Results;
using FluentValidation.Validators;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.PersonsApi.ViewModel.Delete;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Validations
{
    /// <summary>
    /// Class extension to law suit referenced person validation
    /// </summary>
    public class LawSuitReferencedValidator : PropertyValidator
    {
        private readonly ILawSuitsApiServiceClient _lawSuitsApiServiceClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiDbContext"></param>
        public LawSuitReferencedValidator(ILawSuitsApiServiceClient lawSuitsApiServiceClient)
        {
            this._lawSuitsApiServiceClient = lawSuitsApiServiceClient;
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
            var value = context.PropertyValue as DeletePersonCommand;
            var propName = "DeletePersonModel";

            var failure = new ValidationFailure(
                propName,
                $"{context.PropertyName} '{value?.Data?.PersonId}' is used in Law Suits."
            );

            failure.ErrorCode = "LawSuitsReferencedValidator";

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
            var value = context.PropertyValue as DeletePersonCommand;

            if (value == null)
            {
                return false;
            }

            var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = value.AccessToken };
            var countReference = await _lawSuitsApiServiceClient.GetCountByResponsibleIdAsync(httpPayload, value.Data.PersonId, ct);
            if (countReference > 0)
            {
                return false;
            }

            return true;
        }
    }
}
