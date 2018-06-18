using Database.Enumerations;

namespace StudentManagement.ViewModels.Student
{
    public class EditStudentViewModel
    {
        #region Properties
        
        /// <summary>
        /// Phone number of student
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Whether student is active or not.
        /// </summary>
        public MasterItemStatus? Status { get; set; }

        #endregion
    }
}