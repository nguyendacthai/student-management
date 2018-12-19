using System.Collections.Generic;

namespace SharedService.ViewModels
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