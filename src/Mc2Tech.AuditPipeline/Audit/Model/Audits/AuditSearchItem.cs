using System;

namespace Mc2Tech.Pipelines.Audit.Model.Audits
{
    public class AuditSearchItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ExternalReference { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public long ExecutionTimeInMs { get; set; }
    }
}