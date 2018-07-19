using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudentManagement.Interfaces.Repositories.UserRole;

namespace StudentManagement.Repositories.UserRole
{
    public class RepositoryUserRole : ParentRepository<Database.Models.Entities.UserRole>, IRepositoryUserRole
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryUserRole(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}