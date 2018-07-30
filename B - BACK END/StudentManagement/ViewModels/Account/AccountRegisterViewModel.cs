using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Database.Enumerations;

namespace StudentManagement.ViewModels.Account
{
    public class AccountRegisterViewModel
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

        #endregion
    }
}