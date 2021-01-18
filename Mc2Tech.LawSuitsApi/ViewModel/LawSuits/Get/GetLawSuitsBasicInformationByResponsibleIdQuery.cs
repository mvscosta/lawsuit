using Mc2Tech.Crosscutting.ViewModel.Base;
using Mc2Tech.LawSuitsApi.Model.LawSuits;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetLawSuitsBasicInformationByResponsibleIdQuery : Query<List<LawSuit>>
    {
        public Guid ResponsibleId { get; set; }
    }
}
