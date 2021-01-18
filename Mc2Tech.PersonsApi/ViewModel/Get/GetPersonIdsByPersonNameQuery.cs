using Mc2Tech.Crosscutting.ViewModel.Base;
using System;
using Mc2Tech.PersonsApi.Model;
using System.Collections.Generic;

namespace Mc2Tech.PersonsApi.ViewModel.Get
{
    public class GetPersonIdsByPersonNameQuery : Query<List<Guid>>
    {
        public string PersonName { get; set; }
    }
}
