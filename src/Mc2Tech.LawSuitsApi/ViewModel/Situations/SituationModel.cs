using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.Situations
{
    public class SituationModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsClosed { get; set; }
    }
}
