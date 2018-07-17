using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Models;
using StudentManagement.Enumerations;

namespace StudentManagement.ViewModels.Attachment
{
    public class SearchAttachmentViewModel
    {
        /// <summary>
        /// Index of attachment
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// Id of student
        /// </summary>
        public List<int> StudentIds { get; set; }


        /// <summary>
        /// Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Sort direction and property.
        /// </summary>
        public Sorting<AttachmentPropertySort> Sort { get; set; }
    }
}