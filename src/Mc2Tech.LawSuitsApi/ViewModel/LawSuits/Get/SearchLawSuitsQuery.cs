using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.Crosscutting.ViewModel.Base;
using System.Collections.Generic;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class SearchLawSuitsQuery : Query<IEnumerable<LawSuit>>
    {
        public DateTime? DistributedDateStart { get; set; }
        
        public DateTime? DistributedDateEnd { get; set; }

        public string ClientPhysicalFolder { get; set; }

        public bool? JusticeSecret { get; set; }

        public Guid? SituationId { get; set; }

        public string ResponsibleName { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
