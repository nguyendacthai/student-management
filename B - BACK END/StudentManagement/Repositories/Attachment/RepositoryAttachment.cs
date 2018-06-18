using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudentManagement.Interfaces.Repositories.Attachment;

namespace StudentManagement.Repositories.Attachment
{
    public class RepositoryAttachment : ParentRepository<Database.Models.Entities.Attachment>,
        IRepositoryAttachment
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryAttachment(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}