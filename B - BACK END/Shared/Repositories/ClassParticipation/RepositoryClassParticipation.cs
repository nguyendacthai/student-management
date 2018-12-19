using System.Data.Entity;
using Shared.Interfaces.Repositories.ClassParticipation;

namespace Shared.Repositories.ClassParticipation
{
    public class RepositoryClassParticipation : ParentRepository<Database.Models.Entities.ClassParticipation>,
        IRepositoryClassParticipation
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryClassParticipation(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}