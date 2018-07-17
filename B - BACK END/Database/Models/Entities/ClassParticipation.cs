using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models.Entities
{
    [Table(nameof(ClassParticipation))]
    public class ClassParticipation
    {
        #region Properties

        /// <summary>
        /// Id of class participation
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Student index
        /// </summary>
        [Required]
        public int StudentId { get; set; }

        /// <summary>
        /// Class index
        /// </summary>
        [Required]
        public int ClassId { get; set; }

        #endregion

        #region Navigators

        /// <summary>
        /// Student who studies this class.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

        /// <summary>
        /// Class
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(ClassId))]
        public virtual Class Class { get; set; }

        #endregion
    }
}
