using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using System;
using System.Collections.Generic;

namespace Mc2Tech.Crosscutting.ViewModel.LawSuits
{
    public class LawSuitModel : ILawSuitDto
    {
        public Guid? Id { get; set; }

        public string UnifiedProcessNumber { get; set; }

        public DateTime? DistributedDate { get; set; }

        public string ClientPhysicalFolder { get; set; }

        public string Description { get; set; }

        public Guid? SituationId { get; set; }

        public Guid? ParentLawSuitId { get; set; }

        public bool? JusticeSecret { get; set; }

        public ILawSuitDto ParentLawSuit { get; set; }

        public IEnumerable<ILawSuitResponsibleDto> LawSuitResponsibles { get; set; }
    }
}
