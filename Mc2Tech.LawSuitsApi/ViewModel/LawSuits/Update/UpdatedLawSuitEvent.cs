using Mc2Tech.Crosscutting.ViewModel.Base;
using Mc2Tech.Crosscutting.ViewModel.LawSuits;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class UpdatedLawSuitEvent : Event
    {
        public Guid LawSuitId { get; set; }

        public Guid ExternalReference { get; set; }

        public string UnifiedProcessNumber { get; set; }

        public IEnumerable<LawSuitResponsibleModel> LawSuitResponsibles { get; set; }
    }
}

