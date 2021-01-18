using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class DeletedLawSuitEvent : Event
    {
        public string UnifiedProcessNumber { get; set; }
    }
}

