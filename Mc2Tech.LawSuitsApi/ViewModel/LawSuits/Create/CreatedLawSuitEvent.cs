using Mc2Tech.Crosscutting.ViewModel.Base;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class CreatedLawSuitEvent : Event
    {
        public Guid LawSuitId { get; set; }

        public Guid ExternalReference { get; set; }

        public string UnifiedProcessNumber { get; set; }

        public IEnumerable<Guid> LawSuitResponsibles { get; set; }
    }
}
