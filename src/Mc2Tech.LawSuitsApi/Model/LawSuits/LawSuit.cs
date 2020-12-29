using Mc2Tech.LawSuitsApi.Model.Situations;
using Mc2Tech.Crosscutting.Model.Base;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.Model.LawSuits
{
    public class LawSuit : BaseObject
    {
        public string UnifiedProcessNumber { get; set; }

        public DateTime DistributedDate { get; set; }

        public string ClientPhysicalFolder { get; set; }

        public string Description { get; set; }

        public Guid SituationId { get; set; }

        public Guid? ParentLawSuitId { get; set; }

        public bool JusticeSecret { get; set; }

        public LawSuit ParentLawSuit { get; set; }

        public Situation Situation { get; set; }

        public IEnumerable<LawSuit> ChildLawSuits { get; set; }

        public IEnumerable<LawSuitResponsible> Responsibles { get; set; }
    }
}
