using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels.Class
{
    public class AddClassViewModel
    {
        #region Properties

        /// <summary>
        /// Id of specialized
        /// </summary>
        [Required]
        public int SpecializedId { get; set; }

        /// <summary>
        /// Name of class
        /// </summary>
        [Required]
        public string Name { get; set; }

        #endregion
    }
}