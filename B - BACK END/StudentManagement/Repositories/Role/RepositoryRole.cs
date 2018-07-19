using System.Data.Entity;
using StudentManagement.Interfaces.Repositories.Role;

namespace StudentManagement.Repositories.Role
{
    public class RepositoryRole : ParentRepository<Database.Models.Entities.Role>,
        IRepositoryRole
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryRole(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}