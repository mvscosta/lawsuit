using System;

namespace Mc2Tech.Crosscutting.Model.Base
{
    public class BaseEntity : BaseObject
    {
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Creation User Id
        /// </summary>
        public Guid? CreationUserId { get; set; }
        /// <summary>
        /// Modified User Id
        /// </summary>
        public Guid? ModifiedUserId { get; set; }
        /// <summary>
        /// Is System Defined related to base system
        /// </summary>
        public bool IsSystemDefined { get; set; }

        ///// <summary>
        ///// Creation User reference
        ///// </summary>
        //public virtual User CreationUser { get; set; }
        ///// <summary>
        ///// Modified User reference
        ///// </summary>
        //public virtual User ModifiedUser { get; set; }

    }
}
