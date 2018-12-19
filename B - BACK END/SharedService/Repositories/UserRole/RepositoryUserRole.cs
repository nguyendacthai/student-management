using System.Data.Entity;
using SharedService.Interfaces.Repositories.UserRole;

namespace SharedService.Repositories.UserRole
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