using Mc2Tech.Crosscutting.Model.Base;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.Model.DALEntity
{
    public class LawSuitEntity : BaseEntity
    {
        public string UnifiedProcessNumber { get; set; }

        public DateTime DistributedDate { get; set; }

        public string ClientPhysicalFolder { get; set; }

        public string Description { get; set; }

        public Guid SituationId { get; set; }

        public Guid? ParentLawSuitId { get; set; }

        public bool JusticeSecret { get; set; }

        public LawSuitEntity ParentLawSuit { get; set; }

        public List<LawSuitEntity> ChildLawSuits { get; set; }

        public List<LawSuitResponsibleEntity> LawSuitResponsibles { get; set; }
    }
}
