using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.PersonsApi.ViewModel.Get
{
    public class GetPersonIdExistsQuery : Query<bool>
    {
        public Guid PersonId { get; set; }
    }
}
