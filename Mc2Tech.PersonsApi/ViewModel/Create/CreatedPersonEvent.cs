using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.PersonsApi.ViewModel.Create
{
    public class CreatedPersonEvent : Event
    {
        public Guid PersonId { get; set; }
    }
}
