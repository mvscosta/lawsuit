using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.Situations
{
    public class CreatedSituationEvent : Event
    {
        public Guid SituationId { get; set; }

        public Guid ExternalReference { get; set; }

    }
}
