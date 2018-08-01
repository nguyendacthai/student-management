using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enumerations;

namespace Database.Models.Entities
{
    [Table(nameof(Student))]
    public class Student
    {
        #region Properties

        /// <summary>
        /// Id of student
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Username of student
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Password of student
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Name of student
        /// </summary>
        [Required]
        public string Fullname { get; set; }

        /// <summary>
        /// Gender of student
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Email of student
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of student
        /// </summary>
        [StringLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// Check student forget password or not. Default = false
        /// </summary>
        public bool ForgotPassword { get; set; }

        /// <summary>
        /// Whether student is active or not.
        /// </summary>
        [Required]
        public MasterItemStatus Status { get; set; }

        #endregion
    }
}
