using System;

namespace Mc2Tech.Crosscutting.Model.ServiceClient
{
    public class SearchFilterDto
    {
        /// <summary>
        /// Name of the property to filter on
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Filter term type to apply to the filtering operation
        /// </summary>
        public object Term { get; set; }

        public Type Type { get; set; }

        /// <summary>
        /// Filter operator to apply
        /// </summary>
        public FilterOperatorDto Operator { get; set; }

        public string Func { get; set; }
    }
}
