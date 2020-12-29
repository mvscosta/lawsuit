using System;

namespace Mc2Tech.Pipelines.Audit.Model.Audits
{
    public class CommandEntity
    {
        public Guid Id { get; set; }

        public Guid ExternalReference { get; set; }

        public string Name { get; set; }

        public string Payload { get; set; }

        public string Result { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public TimeSpan ExecutionTime { get; set; }
    }
}
