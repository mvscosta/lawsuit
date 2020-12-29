using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetLawSuitByUnifiedProcessNumberQuery : Query<LawSuit>
    {
        public string UnifiedProcessNumber { get; set; }
    }
}
