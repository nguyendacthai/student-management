using System.Data.Entity;
using Shared.Interfaces.Repositories.Student;

namespace Shared.Repositories.Student
{
    public class RepositoryStudent : ParentRepository<Database.Models.Entities.Student>,
        IRepositoryStudent
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryStudent(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}