using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Validations.LawSuits
{
    /// <summary>
    /// Class to validate if the responsibles informed is valid
    /// </summary>
    public class LawSuitUnifiedProcessNumberValidator : PropertyValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LawSuitUnifiedProcessNumberValidator()
        {
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
                $"Law Suit Unified Process Number is not valid"
            );

            failure.ErrorCode = "LawSuitUnifiedProcessNumberValidator";

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
            var value = context.PropertyValue as string;

            if (value == null || value.Length != 20 || !Regex.IsMatch(value, @"^\d+$"))
            {
                return false;
            }

            var origem = value.Substring(16,4);
            var jtr = value.Substring(13,3);
            var ano = value.Substring(9,4);
            var digitoVerificador = value.Substring(7,2);
            var numero = Convert.ToDecimal(value.Substring(0, 7));

            var R1 = numero % 97;
            var R2= Convert.ToDecimal($"{R1}{ano}{jtr}") % 97;
            var R3 = Convert.ToDecimal($"{R2}{origem}{digitoVerificador}") % 97;
            
            var isValid = R3 == 1;                

            return isValid;
        }
    }
}
