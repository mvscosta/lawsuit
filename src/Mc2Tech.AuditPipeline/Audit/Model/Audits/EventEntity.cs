using System;

namespace Mc2Tech.Pipelines.Audit.Model.Audits
{
    public class EventEntity
    {
        public Guid Id { get; set; }

        public Guid CommandId { get; set; }

        public Guid ExternalReference { get; set; }

        public string Name { get; set; }

        public string Payload { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
