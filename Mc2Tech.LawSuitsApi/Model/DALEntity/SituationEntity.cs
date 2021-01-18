using Mc2Tech.Crosscutting.Model.Base;

namespace Mc2Tech.LawSuitsApi.Model.DALEntity
{
    public class SituationEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsClosed { get; set; }
    }
}
