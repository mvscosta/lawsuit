using Mc2Tech.Crosscutting.Model.ServiceClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.Crosscutting.Interfaces.ServiceClient
{
    public interface IPersonsApiServiceClient
    {
        Task<List<Guid>> GetPersonIdsByPersonNameAsync(HttpRequestPayloadDto httpRequestPayload, string personName, CancellationToken ct);
    }
}
