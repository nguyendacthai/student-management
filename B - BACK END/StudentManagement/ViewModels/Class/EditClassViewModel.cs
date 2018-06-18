using Database.Enumerations;

namespace StudentManagement.ViewModels.Class
{
    public class EditClassViewModel
    {
        #region Properties

        /// <summary>
        /// Id of specialized
        /// </summary>
        public int SpecializedId { get; set; }

        /// <summary>
        /// Name of specialized
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether class is active or not.
        /// </summary>
        public MasterItemStatus? Status { get; set; }

        #endregion
    }
}