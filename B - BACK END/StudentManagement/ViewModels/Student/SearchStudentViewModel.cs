using System.Collections.Generic;
using Database.Enumerations;
using Shared.Models;
using StudentManagement.Enumerations;

namespace StudentManagement.ViewModels.Student
{
    public class SearchStudentViewModel
    {
        /// <summary>
        /// Index of student
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// Name of student
        /// </summary>
        public List<string> Usernames { get; set; }

        /// <summary>
        /// Full name of student
        /// </summary>
        public List<string> Fullnames { get; set; }

        /// <summary>
        /// Gender of student
        /// </summary>
        public List<Gender> Genders { get; set; }

        /// <summary>
        /// Status of student.
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