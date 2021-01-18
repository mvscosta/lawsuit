using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.Situations
{
    public class UpdateSituationModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public bool IsClosed { get; set; }
    }
}
