using FluentValidation;
using FluentValidation.Results;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.PersonsApi.ViewModel.Delete;

namespace Mc2Tech.Validations.LawSuits
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator(ILawSuitsApiServiceClient lawSuitsApiServiceClient)
        {
            RuleFor(p => p.Data)
                .NotEmpty();
            RuleFor(p => p)
                .CustomAsync(async (a, customContext, ct) =>
                {
                    var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = a.AccessToken };
                    var countReference = await lawSuitsApiServiceClient.GetCountByResponsibleIdAsync(httpPayload, a.Data.PersonId, ct);
                    if (countReference > 0)
                    {
                        customContext.AddFailure(
                            new ValidationFailure(
                                customContext.PropertyName,
                                $"Person Id '{a}' not allowed for deletion. Used in Law Suit."
                            )
                            { ErrorCode = "PersonValidator" }
                        );
                    }
                });
        }
    }
}

