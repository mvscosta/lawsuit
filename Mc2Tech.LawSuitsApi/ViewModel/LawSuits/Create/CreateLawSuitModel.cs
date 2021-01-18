using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mc2Tech.LawSuitsApi.ViewModel.LawSuits
{
    /// <summary>
    /// Create Law Suit Model Object
    /// </summary>
    public class CreateLawSuitModel
    {
        /// <summary>
        /// Unified Process Number
        /// </summary>
        [Required]
        [MinLength(20)]
        [MaxLength(20)]
        public string UnifiedProcessNumber { get; set; }

        /// <summary>
        /// Distributed Date
        /// </summary>
        public DateTime DistributedDate { get; set; }

        /// <summary>
        /// Client Physical Folder
        /// </summary>
        public string ClientPhysicalFolder { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Situation Id
        /// </summary>
        public Guid? SituationId { get; set; }
        
        /// <summary>
        /// Parent Law Suit Id
        /// </summary>
        public Guid? ParentLawSuitId { get; set; }

        /// <summary>
        /// Law Suit Justice Secret
        /// </summary>
        public bool JusticeSecret { get; set; }

        /// <summary>
        /// Law Suit Responsibles Ids
        /// </summary>
        [MaxLength(3)]
        public IEnumerable<Guid> LawSuitResponsibles { get; set; }
    }
}
