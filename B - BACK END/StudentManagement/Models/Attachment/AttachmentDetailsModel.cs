using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.Models.Attachment
{
    public class AttachmentDetailsModel
    {
        /// <summary>
        /// Attachment's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's id
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Attachment's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attachment's type
        /// </summary>
        public string Type { get; set; }

    }
}