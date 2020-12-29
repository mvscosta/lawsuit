using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.Crosscutting.ServiceClients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.ServiceClient
{
    public class LawSuitsApiServiceClient : BaseServiceClient<ILawSuitModel>, ILawSuitsApiServiceClient
    {
        public LawSuitsApiServiceClient(ApiEndpoints apiEndpoints, IHttpClientFactory clientFactory) 
            :base(apiEndpoints, clientFactory)
        {
        }

        protected override string AdaptiveUri => this.ApiEndpoints.LawSuitsApiUri;

        public async Task<int> GetCountByResponsibleIdAsync(HttpRequestPayloadDto httpRequestPayload, System.Guid responsibleId, CancellationToken ct)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/LawSuits/GetCountByResponsibleIdAsync/" + responsibleId);

            return JsonSerializer.Deserialize<int>(json);
        }

        public async Task<List<Guid>> GetResponsibleIdsByUnifiedProcessNumberAsync(HttpRequestPayloadDto httpRequestPayload, string unifiedProcessNumber, CancellationToken ct)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/LawSuits/GetResponsibleIdsByUnifiedProcessNumberAsync/" + unifiedProcessNumber);

            return JsonSerializer.Deserialize<List<Guid>>(json);
        }
    }
}
