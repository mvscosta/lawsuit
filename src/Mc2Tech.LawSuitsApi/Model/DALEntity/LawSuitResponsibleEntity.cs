using Mc2Tech.Crosscutting.Model.Base;
using System;

namespace Mc2Tech.LawSuitsApi.Model.DALEntity
{
    public class LawSuitResponsibleEntity : BaseEntity
    {
        public Guid LawSuitId { get; set; }

        public LawSuitEntity LawSuit { get; set; }

        public Guid PersonId { get; set; }
    }
}
