using System.Data.Entity;
using SharedService.Interfaces.Repositories.Attachment;

namespace SharedService.Repositories.Attachment
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