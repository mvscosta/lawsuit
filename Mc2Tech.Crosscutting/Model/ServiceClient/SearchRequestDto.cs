using System.Collections.Generic;

namespace Mc2Tech.Crosscutting.Model.ServiceClient
{
    public class SearchRequestDto
    {
        /// <summary>
        /// Number of the page required starting from 1
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Maximum number of records to retrieve   
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Collection of sort descriptors to apply to the data set
        /// </summary>
        public IList<SearchSortDto> SortDescriptors { get; set; }

        /// <summary>
        /// Collection of filter descriptors to apply to the data set
        /// </summary>
        public IList<SearchFilterDto> FilterDescriptors { get; set; }
    }
}