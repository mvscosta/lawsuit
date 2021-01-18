using Mc2Tech.LawSuitsApi.Model.Situations;
using Mc2Tech.Crosscutting.ViewModel.Base;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class SearchSituationsQuery : Query<IEnumerable<Situation>>
    {
        public string FilterQ { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
