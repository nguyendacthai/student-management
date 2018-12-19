using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shared.Enumerations;
using Shared.Models;
using SharedService.Interfaces.Repositories;
using SharedService.Interfaces.Repositories.Attachment;
using SharedService.Interfaces.Repositories.Class;
using SharedService.Interfaces.Repositories.ClassParticipation;
using SharedService.Interfaces.Repositories.Role;
using SharedService.Interfaces.Repositories.Specialized;
using SharedService.Interfaces.Repositories.Student;
using SharedService.Interfaces.Repositories.UserRole;
using SharedService.Repositories.Attachment;
using SharedService.Repositories.Class;
using SharedService.Repositories.ClassParticipation;
using SharedService.Repositories.Role;
using SharedService.Repositories.Specialized;
using SharedService.Repositories.Student;
using SharedService.Repositories.UserRole;

namespace SharedService.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructor

        /// <summary>
        ///     Initiate unit of work with database context.
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Context which is used for accessing into database.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Repository which provides function to access into Attachment.
        /// </summary>
        private IRepositoryAttachment _repositoryAttachment;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into Attachment.
        /// </summary>
        public IRepositoryAttachment RepositoryAttachment =>
            _repositoryAttachment ?? (_repositoryAttachment =
                new RepositoryAttachment(_dbContext));

        /// <summary>
        ///     Repository which provides function to access into Class.
        /// </summary>
        private IRepositoryClass _repositoryClass;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into Class.
        /// </summary>
        public IRepositoryClass RepositoryClass =>
            _repositoryClass ?? (_repositoryClass =
                new RepositoryClass(_dbContext));

        /// <summary>
        ///     Repository which provides function to access into Class Participation.
        /// </summary>
        private IRepositoryClassParticipation _repositoryClassParticipation;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into Class Participation.
        /// </summary>
        public IRepositoryClassParticipation RepositoryClassParticipation =>
            _repositoryClassParticipation ?? (_repositoryClassParticipation =
                new RepositoryClassParticipation(_dbContext));

        /// <summary>
        ///     Repository which provides function to access into Specialized.
        /// </summary>
        private IRepositorySpecialized _repositorySpecialized;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into Specialized.
        /// </summary>
        public IRepositorySpecialized RepositorySpecialized =>
            _repositorySpecialized ?? (_repositorySpecialized =
                new RepositorySpecialized(_dbContext));

        /// <summary>
        ///     Repository which provides function to access into Class.
        /// </summary>
        private IRepositoryStudent _repositoryStudent;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into Class.
        /// </summary>
        public IRepositoryStudent RepositoryStudent =>
            _repositoryStudent ?? (_repositoryStudent =
                new RepositoryStudent(_dbContext));

        /// <summary>
        ///     Repository which provides function to access into Role.
        /// </summary>
        private IRepositoryRole _repositoryRole;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into Role.
        /// </summary>
        public IRepositoryRole RepositoryRole =>
            _repositoryRole ?? (_repositoryRole =
                new RepositoryRole(_dbContext));

        /// <summary>
        ///     Repository which provides function to access into User Role.
        /// </summary>
        private IRepositoryUserRole _repositoryUserRole;

        /// <inheritdoc />
        /// <summary>
        ///     Repository which provides functions to access into User Role.
        /// </summary>
        public IRepositoryUserRole RepositoryUserRole =>
            _repositoryUserRole ?? (_repositoryUserRole =
                new RepositoryUserRole(_dbContext));

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        ///     Commit changes to database.
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Commit changes to database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Undo changes in unit of work
        /// </summary>
        public void Undo(EntryUndo undo)
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        if (undo.HasFlag(EntryUndo.Modified))
                            entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        if (undo.HasFlag(EntryUndo.Added))
                            entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        if (undo.HasFlag(EntryUndo.Deleted))
                            entry.Reload();
                        break;
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Revert changes.
        /// </summary>
        public void Revert()
        {
            var context = ((IObjectContextAdapter)_dbContext).ObjectContext;
            var refreshableObjects = _dbContext.ChangeTracker.Entries().Select(c => c.Entity).ToList();
            context.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IQueryable<T> Paginate<T>(IQueryable<T> list, Pagination pagination)
        {
            if (pagination == null)
                return list;

            // Calculate page index.
            var index = pagination.Page - 1;
            if (index < 0)
                index = 0;

            return Queryable.Take(list.Skip(index * pagination.Records), pagination.Records);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public IQueryable<T> Paginate<T>(IQueryable<T> list, int page, int records)
        {
            // Calculate page index.
            var index = page - 1;
            if (index < 0)
                index = 0;

            return list.Skip(index * records).Take(records);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sort a list by using specific property enumeration.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortProperty"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Sort<TEntity>(IQueryable<TEntity> list, SortDirection sortDirection,
            Enum sortProperty)
        {
            string sortMethod;
            if (sortDirection == SortDirection.Ascending)
                sortMethod = "OrderBy";
            else
                sortMethod = "OrderByDescending";

            // FullSearch parameter expression.
            var parameterExpression = Expression.Parameter(list.ElementType, "p");

            // FullSearch name of property which should be used for sorting.
            var sortPropertyName = Enum.GetName(sortProperty.GetType(), sortProperty);
            if (string.IsNullOrEmpty(sortPropertyName))
                return list;

            // FullSearch member expression.
            var memberExpression = Expression.Property(parameterExpression, sortPropertyName);

            var lamdaExpression = Expression.Lambda(memberExpression, parameterExpression);

            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                sortMethod,
                new[] { list.ElementType, memberExpression.Type },
                list.Expression,
                Expression.Quote(lamdaExpression));

            return list.Provider.CreateQuery<TEntity>(methodCallExpression);
        }

        #endregion
    }
}