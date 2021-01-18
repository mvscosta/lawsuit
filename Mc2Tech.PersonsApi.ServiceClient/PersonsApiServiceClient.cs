using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.Crosscutting.Model;
using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.Crosscutting.ServiceClients;
using Mc2Tech.Crosscutting.ViewModel.Persons;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.ServiceClient
{
    public class PersonsApiServiceClient : BaseServiceClient<ILawSuitDto>, IPersonsApiServiceClient
    {
        public PersonsApiServiceClient(ApiEndpoints apiEndpoints, IHttpClientFactory clientFactory) 
            :base(apiEndpoints, clientFactory)
        {
        }

        protected override string AdaptiveUri => this.ApiEndpoints.PersonsApiUri;

        public async Task<List<Guid>> GetPersonIdsByPersonNameAsync(HttpRequestPayloadDto httpRequestPayload, string personName, CancellationToken ct)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/Persons/GetPersonIdsByPersonName/" + personName);

            return JsonSerializer.Deserialize<List<Guid>>(json);
        }

        public async Task<bool> GetPersonIdExistsAsync(HttpRequestPayloadDto httpRequestPayload, Guid personId, CancellationToken ct)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/Persons/GetPersonIdExists/" + personId);

            return JsonSerializer.Deserialize<bool>(json);
        }
        
        public async Task<PersonModel> GetPersonBasicInformationAsync(HttpRequestPayloadDto httpRequestPayload, Guid personId, CancellationToken ct)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/Persons/GetPersonBasicInformation/" + personId);

            return JsonSerializer.Deserialize<PersonModel>(json);
        }
    }
}
