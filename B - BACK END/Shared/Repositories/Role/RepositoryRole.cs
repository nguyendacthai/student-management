using System.Data.Entity;
using Shared.Interfaces.Repositories.Role;

namespace Shared.Repositories.Role
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