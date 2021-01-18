using System;

namespace Mc2Tech.Pipelines.Audit.Model.Audits
{
    public class AuditEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public object Payload { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}