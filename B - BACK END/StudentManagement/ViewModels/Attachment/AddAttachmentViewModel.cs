using System.ComponentModel.DataAnnotations;
using ApiMultiPartFormData.Models;

namespace StudentManagement.ViewModels.Attachment
{
    public class AddAttachmentViewModel
    {
        #region Properties

        /// <summary>
        /// Id of student
        /// </summary>
        [Required]
        public int StudentId { get; set; }

        ///// <summary>
        ///// Name of document.
        ///// </summary>
        //[Required]
        //public string Name { get; set; }

        /// <summary>
        /// Document binary content.
        /// </summary>
        [Required]
        public HttpFile Document { get; set; }

        #endregion
    }
}