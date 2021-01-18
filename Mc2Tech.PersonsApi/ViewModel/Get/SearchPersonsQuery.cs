using Mc2Tech.Crosscutting.ViewModel.Base;
using Mc2Tech.PersonsApi.Model;
using System.Collections.Generic;

namespace Mc2Tech.PersonsApi.ViewModel.Get
{
    public class SearchPersonsQuery : Query<IEnumerable<Person>>
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string UnifiedProcessNumber { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
