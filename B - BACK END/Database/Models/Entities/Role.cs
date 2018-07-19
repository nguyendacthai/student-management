using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.Entities
{
    [Table(nameof(Role))]
    public class Role
    {
        #region Properties

        /// <summary>
        /// Role's id
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Role's name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        #endregion
    }
}
