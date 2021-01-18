using Mc2Tech.Crosscutting.Enums;
using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using System;

namespace Mc2Tech.Crosscutting.ViewModel.LawSuits
{
    public class LawSuitResponsibleModel : ILawSuitResponsibleDto
    {
        public Guid? Id { get; set; }

        public Guid LawSuitId { get; set; }

        public Guid PersonId { get; set; }

        public ObjectStatus Status { get; set; }
    }
}
