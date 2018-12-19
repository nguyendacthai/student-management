using System.Data.Entity;
using Shared.Interfaces.Repositories.UserRole;

namespace Shared.Repositories.UserRole
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