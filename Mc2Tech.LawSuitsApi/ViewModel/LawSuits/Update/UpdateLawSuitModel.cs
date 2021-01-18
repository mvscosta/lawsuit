using Mc2Tech.Crosscutting.ViewModel.LawSuits;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    public class UpdateLawSuitModel
    {
        public Guid Id { get; set; }

        public DateTime? DistributedDate { get; set; }

        public string ClientPhysicalFolder { get; set; }

        public string Description { get; set; }

        public Guid? SituationId { get; set; }

        public Guid? ParentLawSuitId { get; set; }

        public bool? JusticeSecret { get; set; }

        /// <summary>
        /// Law Suit Responsibles
        /// </summary>
        [MaxLength(3)]
        public IEnumerable<LawSuitResponsibleModel> LawSuitResponsibles { get; set; }
    }
}
