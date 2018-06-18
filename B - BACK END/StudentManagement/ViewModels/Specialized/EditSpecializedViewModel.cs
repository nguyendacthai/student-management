using Database.Enumerations;

namespace StudentManagement.ViewModels.Specialized
{
    public class EditSpecializedViewModel
    {
        #region Properties

        /// <summary>
        /// Name of specialized
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether specialized is active or not.
        /// </summary>
        public MasterItemStatus? Status { get; set; }

        #endregion
    }
}