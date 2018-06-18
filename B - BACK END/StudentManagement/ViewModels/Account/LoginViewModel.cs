using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels.Account
{
    public class LoginViewModel
    {
        #region Properties

        /// <summary>
        ///     Account name.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        ///     Password belongs to username.
        /// </summary>
        [Required]
        public string Password { get; set; }

        #endregion
    }
}