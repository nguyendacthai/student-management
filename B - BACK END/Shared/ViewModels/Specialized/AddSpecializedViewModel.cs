using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels.Specialized
{
    public class AddSpecializedViewModel
    {
        #region Properties

        /// <summary>
        /// Name of specialized
        /// </summary>
        [Required]
        public string Name { get; set; }

        #endregion
    }
}
