using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace Mc2Tech.BaseApi.Controllers
{
    public abstract class Mc2TechControllerBase : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public AuthenticationHeaderValue GetAccessToken()
        {
            var authorizationHeader = Request.Headers.ContainsKey(HeaderNames.Authorization)
                ? AuthenticationHeaderValue.Parse(Request.Headers[HeaderNames.Authorization])
                : null;
            return authorizationHeader;
        }
    }
}
