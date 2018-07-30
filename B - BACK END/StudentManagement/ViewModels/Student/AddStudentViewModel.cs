using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Enumerations;

namespace StudentManagement.ViewModels.Student
{
    public class AddStudentViewModel
    {
        #region Properties

        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Fullname
        /// </summary>
        [Required]
        public string Fullname { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [Required]
        public List<int> Roles { get; set; }

        #endregion
    }
}