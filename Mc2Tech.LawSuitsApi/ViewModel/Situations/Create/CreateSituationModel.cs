using System;

namespace Mc2Tech.LawSuitsApi.ViewModel.Situations
{
    public class CreateSituationModel
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public bool IsClosed { get; set; }
    }
}
