using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudentManagement.Interfaces.Repositories.Student;

namespace StudentManagement.Repositories.Student
{
    public class RepositoryStudent : ParentRepository<Database.Models.Entities.Student>,
        IRepositoryStudent
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryStudent(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}