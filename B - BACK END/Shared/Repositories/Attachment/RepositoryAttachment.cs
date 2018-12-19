using System.Data.Entity;
using Shared.Interfaces.Repositories.Attachment;

namespace Shared.Repositories.Attachment
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