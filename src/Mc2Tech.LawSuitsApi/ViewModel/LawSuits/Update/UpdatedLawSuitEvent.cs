using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class UpdatedLawSuitEvent : Event
    {
        public string UnifiedProcessNumber { get; set; }
    }
}

