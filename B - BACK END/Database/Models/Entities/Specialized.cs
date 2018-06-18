using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enumerations;

namespace Database.Models.Entities
{
    [Table(nameof(Specialized))]
    public class Specialized
    {
        #region Properties

        /// <summary>
        /// Id of specialized
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of specialized
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Whether specialized is active or not.
        /// </summary>
        [Required]
        public MasterItemStatus Status { get; set; }

        #endregion

    }
}
