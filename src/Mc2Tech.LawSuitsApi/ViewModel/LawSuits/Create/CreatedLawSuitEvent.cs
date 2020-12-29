using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class CreatedLawSuitEvent : Event
    {
        public Guid LawSuitId { get; set; }

        public Guid ExternalReference { get; set; }

        public string UnifiedProcessNumber { get; set; }
    }
}
