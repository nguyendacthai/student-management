using System;
using System.Collections.Generic;

namespace SharedService.Interfaces.Services
{
    public interface IValueCacheService<TKey, TValue>
    {
        #region Methods

        /// <summary>
        /// Add key-value to cache with expiration time.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        void Add(TKey key, TValue value, DateTime? expirationTime = null);

        /// <summary>
        /// Add key-value to cache with expiration time (calculate by using life time (seconds))
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="lifeTime"></param>
        void Add(TKey key, TValue value, int lifeTime);

        /// <summary>
        /// Get template by using specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue Read(TKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<TValue> ReadValues();

        /// <summary>
        /// Remove a value from dictionary.
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);

        /// <summary>
        /// Find key in dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TKey FindKey(TKey key);

        #endregion
    }
}
