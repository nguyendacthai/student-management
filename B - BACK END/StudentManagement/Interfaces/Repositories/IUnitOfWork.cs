using StudentManagement.Interfaces.Repositories.Attachment;
using StudentManagement.Interfaces.Repositories.Class;
using StudentManagement.Interfaces.Repositories.ClassParticipation;
using StudentManagement.Interfaces.Repositories.Specialized;
using StudentManagement.Interfaces.Repositories.Student;

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

        #endregion
    }
}