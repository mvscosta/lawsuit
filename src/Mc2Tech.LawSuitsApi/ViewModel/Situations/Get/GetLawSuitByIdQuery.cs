using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.LawSuitsApi.Model.Situations;
using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetSituationByIdQuery : Query<Situation>
    {
        public Guid SituationId { get; set; }
    }
}
