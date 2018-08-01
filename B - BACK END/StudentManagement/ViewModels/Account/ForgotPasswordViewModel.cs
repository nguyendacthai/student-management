using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        #region Properties

        /// <summary>
        /// User's email
        /// </summary>
        [Required]
        public string Email { get; set; }

        #endregion
    }
}