using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mc2Tech.Crosscutting.Model.ServiceClient;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using IdentityModel.Client;

namespace Mc2Tech.Crosscutting.ServiceClients
{
    public abstract class BaseServiceClient<TDto> : IBaseServiceClient<TDto> where TDto : class
    {
        protected abstract string AdaptiveUri { get; }
        protected readonly IHttpClientFactory ClientFactory;
        protected readonly ApiEndpoints ApiEndpoints;

        protected BaseServiceClient(ApiEndpoints apiEndpoint, IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
            ApiEndpoints = apiEndpoint;
        }

        public async Task<SearchResultDto<TDto>> GetAllAsync(HttpRequestPayloadDto httpRequestPayload, SearchRequestDto request)
        {
            var client = GetClient(httpRequestPayload);

            var postBody = JsonSerializer.Serialize(request);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/GetAll", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SearchResultDto<TDto>>(json);
        }

        public async Task<int> GetCountAsync(HttpRequestPayloadDto payload, SearchRequestDto request)
        {
            var client = GetClient(payload);

            var postBody = JsonSerializer.Serialize(request);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/GetCount", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(json);
        }

        public async Task<TDto> GetByExternalReferenceAsync(HttpRequestPayloadDto httpRequestPayload, string reference)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/GetByExternalReference/" + reference);

            return JsonSerializer.Deserialize<TDto>(json);
        }

        public async Task<TDto> GetByIdAsync(HttpRequestPayloadDto httpRequestPayload, int id)
        {
            var client = GetClient(httpRequestPayload);

            var json = await client.GetStringAsync(AdaptiveUri + "/Get/" + id);
            return JsonSerializer.Deserialize<TDto>(json);
        }

        public async Task<ResultDto<string>> AddAsync(HttpRequestPayloadDto httpRequestPayload, TDto dto)
        {
            var client = GetClient(httpRequestPayload);

            var postBody = JsonSerializer.Serialize(dto);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/Post", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<string>>(content);
        }

        public async Task<ResultDto<bool>> DeleteAsync(HttpRequestPayloadDto httpRequestPayload, string reference)
        {
            var client = GetClient(httpRequestPayload);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.DeleteAsync(AdaptiveUri + "/Delete/" + reference);

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }


        public async Task<ResultDto<bool>> ModifyAsync(HttpRequestPayloadDto httpRequestPayload, TDto dto)
        {
            var client = GetClient(httpRequestPayload);

            var postBody = JsonSerializer.Serialize(dto);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PutAsync(AdaptiveUri + "/Put", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }

        public async Task<ResultDto<bool>> EnableAsync(HttpRequestPayloadDto httpRequestPayload, string reference)
        {
            var client = GetClient(httpRequestPayload);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/Enable/" + reference, null);

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }

        public async Task<ResultDto<bool>> DisableAsync(HttpRequestPayloadDto httpRequestPayload, string reference)
        {
            var client = GetClient(httpRequestPayload);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/Disable/" + reference, null);

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }

        public async Task<ResultDto<bool>> EnableManyAsync(HttpRequestPayloadDto httpRequestPayload, IList<string> references)
        {
            var client = GetClient(httpRequestPayload);

            var postBody = JsonSerializer.Serialize(references);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/EnableMany", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }

        public async Task<ResultDto<bool>> DisableManyAsync(HttpRequestPayloadDto httpRequestPayload, IList<string> references)
        {
            var client = GetClient(httpRequestPayload);

            var postBody = JsonSerializer.Serialize(references);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/DisableMany", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }

        public async Task<ResultDto<bool>> DeleteManyAsync(HttpRequestPayloadDto httpRequestPayload, IList<string> references)
        {
            var client = GetClient(httpRequestPayload);

            var postBody = JsonSerializer.Serialize(references);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.PostAsync(AdaptiveUri + "/DeleteMany", new StringContent(postBody, Encoding.UTF8, "application/json"));

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResultDto<bool>>(content);
        }

        protected HttpClient GetClient(HttpRequestPayloadDto requestPayload)
        {
            var client = ClientFactory.CreateClient();

            client.DefaultRequestHeaders.Clear();

            client.SetBearerToken(requestPayload.AccessToken);

            return client;
        }

        internal class CustomHandler : DelegatingHandler
        {
            public CustomHandler(HttpMessageHandler innerHandler) : base(innerHandler)
            {
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var response = await base.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                    return response;

                var content = await response.Content.ReadAsStringAsync();
                var message = !string.IsNullOrEmpty(content) ? content : response.ReasonPhrase;

                throw new Exception(message);
            }
        }
    }
}