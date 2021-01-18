using Mc2Tech.Crosscutting.ViewModel.Base;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetResponsibleIdsByUnifiedProcessNumberQuery : Query<IEnumerable<Guid>>
    {
        public string UnifiedProcessNumber { get; set; }
    }
}
