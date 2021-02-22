using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.PersonsApi.ViewModel.Delete
{
    public class SoftDeletedPersonEvent : Event
    {
        public Guid PersonId { get; set; }
    }
}

