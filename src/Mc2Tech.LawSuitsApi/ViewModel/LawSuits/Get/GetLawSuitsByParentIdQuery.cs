using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.Crosscutting.ViewModel.Base;
using System.Collections.Generic;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetLawSuitsByParentIdQuery : Query<IEnumerable<LawSuit>>
    {
        public Guid ParentId { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
