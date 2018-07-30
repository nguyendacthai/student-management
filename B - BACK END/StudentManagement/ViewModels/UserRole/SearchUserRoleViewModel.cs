using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database.Enumerations;
using Shared.Models;
using StudentManagement.Enumerations;

namespace StudentManagement.ViewModels.UserRole
{
    public class SearchUserRoleViewModel
    {
        /// <summary>
        /// Indexes
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// Id of student
        /// </summary>
        public List<int> StudentIds { get; set; }

        /// <summary>
        /// Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Sort direction and property.
        /// </summary>
        public Sorting<UserRolePropertySort> Sort { get; set; }
    }
}