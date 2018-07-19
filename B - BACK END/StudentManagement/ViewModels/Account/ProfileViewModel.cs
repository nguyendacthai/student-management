using System.Collections.Generic;

namespace StudentManagement.ViewModels.Account
{
    public class ProfileViewModel : Database.Models.Entities.Student
    {
        #region Properties
        
        /// <summary>
        /// List of user roles.
        /// </summary>
        public List<int> Roles { get; set; }

        #endregion
    }
}