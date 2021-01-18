using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.PersonsApi.ViewModel.Update
{
    public class UpdatedPersonEvent : Event
    {
        public string PersonId { get; set; }
    }
}

