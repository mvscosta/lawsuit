using Mc2Tech.Crosscutting.ViewModel.Base;
using Mc2Tech.Pipelines.Audit.Model.Audits;
using System;

namespace Mc2Tech.BaseApi.ViewModel.Audits
{
    public class GetAuditByIdQuery : Query<Audit>
    {
        public Guid AuditId { get; set; }
    }
}
