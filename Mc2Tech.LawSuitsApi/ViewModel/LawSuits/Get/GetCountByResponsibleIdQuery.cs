using Mc2Tech.Crosscutting.ViewModel.Base;
using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class GetCountByResponsibleIdQuery : Query<int>
    {
        public Guid ResponsibleId { get; set; }
    }
}
