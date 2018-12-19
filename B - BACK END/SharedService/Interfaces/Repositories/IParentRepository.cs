using System.Collections.Generic;
using System.Linq;

namespace SharedService.Interfaces.Repositories
{
    public interface IParentRepository<T>
    {
        #region Methods

        /// <summary>
        ///     FullSearch all data from the specific table.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Search();

        /// <summary>
        ///     Insert a record into data table.
        /// </summary>
        /// <param name="entity"></param>
        T Insert(T entity);

        /// <summary>
        ///     Insert or update a record in table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T InsertOrUpdate(T entity);

        /// <summary>
        ///     Remove a list of entities from database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void Remove(IEnumerable<T> entities);

        /// <summary>
        ///     Remove an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Remove(T entity);

        #endregion
    }
}