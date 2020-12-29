using System;
using System.Collections.Generic;

namespace Mc2Tech.BaseApi.ViewModel.Audits
{
    public class AuditModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public object Payload { get; set; }

        public object Result { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public long ExecutionTimeInMs { get; set; }

        public IEnumerable<AuditEventModel> Events { get; set; }
    }
}