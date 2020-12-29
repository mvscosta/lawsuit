using Mc2Tech.Crosscutting.Model.Base;

namespace Mc2Tech.LawSuitsApi.Model.Situations
{
    public class Situation : BaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsClosed { get; set; }
    }
}
