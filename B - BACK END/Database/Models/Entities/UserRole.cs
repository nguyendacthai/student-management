using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Shared.Enumerations;

namespace Database.Models.Entities
{
    [Table(nameof(UserRole))]
    public class UserRole
    {
        #region Properties

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id of student.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Id of role.
        /// </summary>
        public int RoleId { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// One student belongs to one relationship only.
        /// </summary>
        [ForeignKey(nameof(StudentId))]
        [JsonIgnore]
        public virtual Student Student { get; set; }

        /// <summary>
        /// One role belongs to one relationship only.
        /// </summary>
        [ForeignKey(nameof(RoleId))]
        [JsonIgnore]
        public virtual Role Role { get; set; }

        #endregion
    }
}
