using Mc2Tech.Crosscutting.ViewModel.Base;
using Mc2Tech.Pipelines.Audit.Model.Audits;
using System.Collections.Generic;

namespace Mc2Tech.BaseApi.ViewModel.Audits
{
    public class SearchAuditsQuery : Query<IEnumerable<AuditSearchItem>>
    {
        public string FilterQ { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
