using Mc2Tech.Crosscutting.Enums;
using System;

namespace Mc2Tech.Crosscutting.Model.Base
{
    public class BaseObject
    {
        /// <summary>
        /// Id that used as PK
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Status 
        /// </summary>
        public ObjectStatus Status { get; set; }

        /// <summary>
        /// External Reference, was used to link with external object services
        /// </summary>
        public Guid? ExternalReference { get; set; }
    }
}
