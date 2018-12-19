using System.Data.Entity;
using SharedService.Interfaces.Repositories.ClassParticipation;

namespace SharedService.Repositories.ClassParticipation
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