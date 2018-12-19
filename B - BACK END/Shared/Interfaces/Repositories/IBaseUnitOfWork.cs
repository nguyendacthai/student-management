using System;
using System.Linq;
using System.Threading.Tasks;
using Shared.Enumerations;
using Shared.Models;

namespace Shared.Interfaces.Repositories
{
    public interface IBaseUnitOfWork
    {
        #region Base Methods

        /// <summary>
        ///     Save change to database synchronously.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        ///     Save change to database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Undo changes in unit of work
        /// </summary>
        void Undo(EntryUndo undo);

        /// <summary>
        /// Revert changes that have been done.
        /// </summary>
        void Revert();

        /// <summary>
        ///     Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IQueryable<T> Paginate<T>(IQueryable<T> list, Pagination pagination);

        /// <summary>
        ///     Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        IQueryable<T> Paginate<T>(IQueryable<T> list, int page, int records);

        /// <summary>
        ///     Sort a list by using specific property enumeration.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortProperty"></param>
        /// <returns></returns>
        IQueryable<TEntity> Sort<TEntity>(IQueryable<TEntity> list, SortDirection sortDirection, Enum sortProperty);

        #endregion
    }
}