using System.Data.Entity;
using SharedService.Interfaces.Repositories.Student;

namespace SharedService.Repositories.Student
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