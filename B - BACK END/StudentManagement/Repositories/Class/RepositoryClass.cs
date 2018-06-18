using System.Data.Entity;
using StudentManagement.Interfaces.Repositories.Class;

namespace StudentManagement.Repositories.Class
{
    public class RepositoryClass : ParentRepository<Database.Models.Entities.Class>,
        IRepositoryClass
    {
        #region Constructor

        /// <inheritdoc />
        /// <summary>
        ///     Initiate repository with injectors.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryClass(DbContext dbContext) : base(dbContext)
        {
        }

        #endregion
    }
}