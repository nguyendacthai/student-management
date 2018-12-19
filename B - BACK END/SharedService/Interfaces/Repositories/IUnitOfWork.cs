using SharedService.Interfaces.Repositories.Attachment;
using SharedService.Interfaces.Repositories.Class;
using SharedService.Interfaces.Repositories.ClassParticipation;
using SharedService.Interfaces.Repositories.Role;
using SharedService.Interfaces.Repositories.Specialized;
using SharedService.Interfaces.Repositories.Student;
using SharedService.Interfaces.Repositories.UserRole;

namespace SharedService.Interfaces.Repositories
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