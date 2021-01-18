using Microsoft.Extensions.Configuration;

namespace Mc2Tech.Crosscutting.ServiceClients
{
    public class ApiEndpoints
    {
        private readonly IConfiguration _configuration;

        public ApiEndpoints(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string LawSuitsApiHttpClientName => "LawSuitsApi";
        public static string PersonsApiHttpClientName => "PersonsApi";

        public string LawSuitsApiUri => _configuration["ApiEndpoints:LawSuitsUri"];
        public string PersonsApiUri => _configuration["ApiEndpoints:PersonsUri"];
    }
}
