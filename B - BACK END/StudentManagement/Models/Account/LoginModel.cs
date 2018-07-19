using System.Collections.Generic;
using Database.Models.Entities;

namespace StudentManagement.Models.Account
{
    public class LoginModel
    {
        #region Properties

        /// <summary>
        /// User instance.
        /// </summary>
        public Student User { get; set; }

        /// <summary>
        /// Roles related to user.
        /// </summary>
        public List<int> Roles { get; set; }

        #endregion
    }
}