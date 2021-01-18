using FluentValidation.Results;
using FluentValidation.Validators;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Class to validate if the responsibles informed is valid
    /// </summary>
    public class LawSuitResponsibleIdsValidator : PropertyValidator
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly IPersonsApiServiceClient _personsApiServiceClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiDbContext"></param>
        /// <param name="personsApiServiceClient"></param>
        public LawSuitResponsibleIdsValidator(ApiDbContext apiDbContext, IPersonsApiServiceClient personsApiServiceClient)
        {
            this._apiDbContext = apiDbContext;
            this._personsApiServiceClient = personsApiServiceClient;
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
            var failure = new ValidationFailure(
                context.PropertyName,
                $"Law Suit responsibles doesn't exist"
            );

            failure.ErrorCode = "LawSuitResponsibleIdsValidator";

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
            var value = context.PropertyValue as CreateLawSuitCommand;

            if ((value?.Data?.LawSuitResponsibles?.Count() ?? 0) == 0)
            {
                return true;
            }

            var isValid = true;
            var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = value.AccessToken };
            
            foreach (var responsible in value.Data.LawSuitResponsibles)
            {
                var found = await _personsApiServiceClient.GetPersonIdExistsAsync(httpPayload, responsible, ct);
                if (!found)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
    }
}
