using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enumerations;

namespace Database.Models.Entities
{
    [Table(nameof(Attachment))]
    public class Attachment
    {
        #region Properties

        /// <summary>
        /// Id of attachment
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
        /// Name of attachment
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Type of attachment
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        /// <summary>
        /// Content of attachment
        /// </summary>
        [Required]
        public byte[] Content { get; set; }

        /// <summary>
        /// Whether attachment is active or not.
        /// </summary>
        [Required]
        public MasterItemStatus Status { get; set; }

        #endregion

        #region Navigators

        /// <summary>
        /// Student who owns this class.
        /// </summary>
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

        #endregion
    }
}
