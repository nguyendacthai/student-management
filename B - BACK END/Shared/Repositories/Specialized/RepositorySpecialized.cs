using System.Data.Entity;
using Shared.Interfaces.Repositories.Specialized;

namespace Shared.Repositories.Specialized
{
    public class RepositorySpecialized : ParentRepository<Database.Models.Entities.Specialized>,
        IRepositorySpecialized
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositorySpecialized(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}