using Mc2Tech.Crosscutting.Model.Base;
using System;
using System.Collections.Generic;

namespace Mc2Tech.LawSuitsApi.Model.LawSuits
{
    public class LawSuitResponsible : BaseObject
    {
        public Guid LawSuitId { get; set; }

        public IEnumerable<LawSuit> LawSuits { get; set; }

        public Guid PersonId { get; set; }
    }
}
