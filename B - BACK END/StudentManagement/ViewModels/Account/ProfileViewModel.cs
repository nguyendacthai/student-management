using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApiMultiPartFormData.Models;

namespace StudentManagement.ViewModels.Account
{
    public class ProfileViewModel
    {
        /// <summary>
        /// Full name of profile.
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// List of attachments.
        /// </summary>
        public List<HttpFile> Attachments { get; set; }
    }
}