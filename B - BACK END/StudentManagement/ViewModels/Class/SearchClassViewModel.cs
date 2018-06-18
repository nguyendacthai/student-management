using System.Collections.Generic;
using Database.Enumerations;
using Shared.Models;
using StudentManagement.Enumerations;

namespace StudentManagement.ViewModels.Class
{
    public class SearchClassViewModel
    {
        /// <summary>
        /// Indexes of specialized
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// Names of specialized
        /// </summary>
        public List<string> Names { get; set; }

        /// <summary>
        /// Statuses of specialized.
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