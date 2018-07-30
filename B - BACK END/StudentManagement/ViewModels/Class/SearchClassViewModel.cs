using System.Collections.Generic;
using Database.Enumerations;
using Shared.Models;
using StudentManagement.Enumerations;

namespace StudentManagement.ViewModels.Class
{
    public class SearchClassViewModel
    {
        /// <summary>
        /// Indexes of class
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// Names of class
        /// </summary>
        public List<string> Names { get; set; }

        /// <summary>
        /// Id of specialized
        /// </summary>
        public List<int> SpecializedIds { get; set; }

        /// <summary>
        /// Statuses of class.
        /// </summary>
        public List<MasterItemStatus> Statuses { get; set; }

        /// <summary>
        /// Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Sort direction and property.
        /// </summary>
        public Sorting<ClassPropertySort> Sort { get; set; }
    }
}