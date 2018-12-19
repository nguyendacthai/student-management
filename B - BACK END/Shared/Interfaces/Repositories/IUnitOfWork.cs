using Shared.Interfaces.Repositories.Attachment;
using Shared.Interfaces.Repositories.Class;
using Shared.Interfaces.Repositories.ClassParticipation;
using Shared.Interfaces.Repositories.Role;
using Shared.Interfaces.Repositories.Specialized;
using Shared.Interfaces.Repositories.Student;
using Shared.Interfaces.Repositories.UserRole;

namespace Shared.Interfaces.Repositories
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        #region Properties

        /// <summary>
        ///     Repository which provides functions to access Attachment database.
        /// </summary>
        IRepositoryAttachment RepositoryAttachment { get; }

        /// <summary>
        ///     Repository which provides functions to access Class database.
        /// </summary>
        IRepositoryClass RepositoryClass { get; }

        /// <summary>
        ///     Repository which provides functions to access Class Participation database.
        /// </summary>
        IRepositoryClassParticipation RepositoryClassParticipation { get; }

        /// <summary>
        ///     Repository which provides functions to access Specialized database.
        /// </summary>
        IRepositorySpecialized RepositorySpecialized { get; }

        /// <summary>
        ///     Repository which provides functions to access Student database.
        /// </summary>
        IRepositoryStudent RepositoryStudent { get; }

        /// <summary>
        ///     Repository which provides functions to access Role database.
        /// </summary>
        IRepositoryRole RepositoryRole { get; }

        /// <summary>
        ///     Repository which provides functions to access UserRole database.
        /// </summary>
        IRepositoryUserRole RepositoryUserRole { get; }

        #endregion
    }
}