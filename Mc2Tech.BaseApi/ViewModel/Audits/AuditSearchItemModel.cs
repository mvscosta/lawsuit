using System;

namespace Mc2Tech.BaseApi.ViewModel.Audits
{
    public class AuditSearchItemModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public long ExecutionTimeInMs { get; set; }
    }
}