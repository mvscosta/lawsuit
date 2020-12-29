using System;

namespace Mc2Tech.Crosscutting.Interfaces.LawSuits
{
    public interface ILawSuitModel
    {
        public Guid Id { get; set; }

        public string UnifiedProcessNumber { get; set; }

        public DateTime DistributedDate { get; set; }

        public string ClientPhysicalFolder { get; set; }

        public string Description { get; set; }

        public Guid SituationId { get; set; }

        public Guid? ParentLawSuitId { get; set; }

        public bool JusticeSecret { get; set; }

        public ILawSuitModel ParentLawSuit { get; set; }
    }
}
