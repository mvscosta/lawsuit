using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.Crosscutting.ServiceClients
{
    public class CustomHandler : DelegatingHandler
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