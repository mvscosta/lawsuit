using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.Crosscutting.ViewModel.Persons;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.Crosscutting.Interfaces.ServiceClient
{
    public interface IPersonsApiServiceClient
    {
        Task<List<Guid>> GetPersonIdsByPersonNameAsync(HttpRequestPayloadDto httpRequestPayload, string personName, CancellationToken ct);
        Task<bool> GetPersonIdExistsAsync(HttpRequestPayloadDto httpRequestPayload, Guid personId, CancellationToken ct);
        Task<PersonModel> GetPersonBasicInformationAsync(HttpRequestPayloadDto httpRequestPayload, Guid personId, CancellationToken ct);
    }
}
