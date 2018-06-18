using System.Collections.Generic;
using Database.Enumerations;
using Shared.Models;
using StudentManagement.Enumerations;

namespace StudentManagement.ViewModels.Student
{
    public class SearchStudentViewModel
    {
        /// <summary>
        /// Indexes of student
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// Names of student
        /// </summary>
        public List<string> Usernames { get; set; }

        /// <summary>
        /// Statuses of student.
        /// </summary>
        public List<MasterItemStatus> Statuses { get; set; }

        /// <summary>
        /// Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Sort direction and property.
        /// </summary>
        public Sorting<StudentPropertySort> Sort { get; set; }
    }
}