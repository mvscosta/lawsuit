using System.Collections.Generic;

namespace Mc2Tech.Crosscutting.Model.ServiceClient
{
    public class SearchResultDto<T> where T : class
    {
        public IList<T> Results { get; set; }

        public int Total { get; set; }
    }
}
