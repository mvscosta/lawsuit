using Mc2Tech.Crosscutting.ViewModel.Base;
using System;
using Mc2Tech.PersonsApi.Model;

namespace Mc2Tech.PersonsApi.ViewModel.Get
{
    public class GetPersonByIdQuery : Query<Person>
    {
        public Guid PersonId { get; set; }
    }
}
