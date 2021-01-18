using Mc2Tech.Crosscutting.Enums;
using System;
using System.Collections.Generic;

namespace Mc2Tech.Crosscutting.Interfaces.LawSuits
{
    public interface ILawSuitResponsibleDto
    {
        public Guid? Id { get; set; }

        public Guid LawSuitId { get; set; }

        public Guid PersonId { get; set; }

        public ObjectStatus Status { get; set; }
    }
}
