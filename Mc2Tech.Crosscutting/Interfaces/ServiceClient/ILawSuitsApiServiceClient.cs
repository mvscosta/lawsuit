using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.Crosscutting.ViewModel.LawSuits;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.Crosscutting.Interfaces.ServiceClient
{
    public interface ILawSuitsApiServiceClient
    {
        Task<int> GetCountByResponsibleIdAsync(HttpRequestPayloadDto httpRequestPayload, Guid responsibleId, CancellationToken ct);

        Task<List<Guid>> GetResponsibleIdsByUnifiedProcessNumberAsync(HttpRequestPayloadDto httpRequestPayload, string unifiedProcessNumber, CancellationToken ct);

        Task<List<LawSuitModel>> GetLawSuitsBasicInformationByResponsibleIdAsync(HttpRequestPayloadDto httpRequestPayload, Guid personId, CancellationToken ct);
    }
}
