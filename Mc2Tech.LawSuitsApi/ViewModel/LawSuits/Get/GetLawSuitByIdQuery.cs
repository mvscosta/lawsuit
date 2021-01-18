using Mc2Tech.Crosscutting.ViewModel.Base;
using Mc2Tech.LawSuitsApi.Model.LawSuits;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetLawSuitByIdQuery : Query<LawSuit>
    {
        public Guid LawSuitId { get; set; }
    }
}