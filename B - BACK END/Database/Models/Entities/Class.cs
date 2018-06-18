using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enumerations;

namespace Database.Models.Entities
{
    [Table(nameof(Class))]
    public class Class
    {
        #region Properties

        /// <summary>
        /// Id of class
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Specialized index
        /// </summary>
        [Required]
        public int SpecializedId { get; set; }

        /// <summary>
        /// Name of class
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Whether class is active or not.
        /// </summary>
        [Required]
        public MasterItemStatus Status { get; set; }

        #endregion

        #region Navigators

        /// <summary>
        /// Specialized which owns this class.
        /// </summary>
        [ForeignKey(nameof(SpecializedId))]
        public virtual Specialized Specialized { get; set; }

        #endregion
    }
}
