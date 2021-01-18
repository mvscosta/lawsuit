using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.Crosscutting.ViewModel.Base;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetResponsibleIdsByLawSuitIdQuery : Query<IEnumerable<Guid>>
    {
        public Guid LawSuitId { get; set; }
    }
}
