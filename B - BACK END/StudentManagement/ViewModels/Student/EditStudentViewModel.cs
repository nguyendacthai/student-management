using Database.Enumerations;

namespace StudentManagement.ViewModels.Student
{
    public class EditStudentViewModel
    {
        #region Properties

//        /// <summary>
//        /// Name of student
//        /// </summary>
//        public string Username { get; set; }
//
//        /// <summary>
//        /// Fullname
//        /// </summary>
//        public string Fullname { get; set; }

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