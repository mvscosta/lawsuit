using System;

namespace Mc2Tech.BaseApi.ViewModel.Audits
{
    public class AuditEventModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public object Payload { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}