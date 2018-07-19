using StudentManagement.Interfaces.Repositories.Attachment;
using StudentManagement.Interfaces.Repositories.Class;
using StudentManagement.Interfaces.Repositories.ClassParticipation;
using StudentManagement.Interfaces.Repositories.Role;
using StudentManagement.Interfaces.Repositories.Specialized;
using StudentManagement.Interfaces.Repositories.Student;
using StudentManagement.Interfaces.Repositories.UserRole;

namespace StudentManagement.Interfaces.Repositories
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