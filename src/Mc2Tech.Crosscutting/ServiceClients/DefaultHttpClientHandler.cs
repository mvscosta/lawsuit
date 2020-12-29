using System.Net;
using System.Net.Http;

namespace Mc2Tech.Crosscutting.ServiceClients
{
    public class DefaultHttpClientHandler : HttpClientHandler
    {
        public DefaultHttpClientHandler() =>
            this.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
    }
}
