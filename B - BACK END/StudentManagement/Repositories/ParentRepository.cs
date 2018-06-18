using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using StudentManagement.Interfaces.Repositories;

namespace StudentManagement.Repositories
{
    public class ParentRepository<T> : IParentRepository<T> where T : class
    {
        #region Properties

        /// <summary>
        ///     Database set.
        /// </summary>
        private readonly DbContext _dbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with database context wrapper.
        /// </summary>
        /// <param name="dbContext"></param>
        public ParentRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search all data from the specific table.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Search()
        {
            return _dbContext.Set<T>();
        }

        /// <summary>
        ///     Insert a record into data table.
        /// </summary>
        /// <param name="entity"></param>
        public T Insert(T entity)
        {
            return _dbContext.Set<T>().Add(entity);
        }

        /// <summary>
        ///     Insert or update a record in table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T InsertOrUpdate(T entity)
        {
            _dbContext.Set<T>().AddOrUpdate(entity);
            return entity;
        }

        /// <summary>
        ///     Remove a list of entities from database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public void Remove(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        ///     Remove an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Remove(T entity)
        {
            return _dbContext.Set<T>().Remove(entity);
        }

        #endregion
    }
}