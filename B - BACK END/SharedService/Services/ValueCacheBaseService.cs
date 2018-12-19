using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SharedService.Interfaces.Services;

namespace SharedService.Services
{
    public class ValueCacheBaseService<TKey, TValue> : IValueCacheService<TKey, TValue>
    {
        #region Properties

        /// <summary>
        /// List of key-value pairs.
        /// </summary>
        private readonly IDictionary<TKey, KeyValuePair<TValue, DateTime?>> _pairs;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize service with specific settings.
        /// </summary>
        public ValueCacheBaseService()
        {
            _pairs = new ConcurrentDictionary<TKey, KeyValuePair<TValue, DateTime?>>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add value to dictionary by using specific key. Override the key if it exists.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        public virtual void Add(TKey key, TValue value, DateTime? expirationTime)
        {
            var actualKey = FindKey(key);
            if (_pairs.ContainsKey(actualKey))
            {
                _pairs[actualKey] = new KeyValuePair<TValue, DateTime?>(value, expirationTime);
                return;
            }

            try
            {
                _pairs.Add(actualKey, new KeyValuePair<TValue, DateTime?>(value, expirationTime));
            }
            catch
            {
                // Suppress error.
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="lifeTime"></param>
        public void Add(TKey key, TValue value, int lifeTime)
        {
            // Get current system time.
            var currentTime = DateTime.Now;

            // Calculate expiration time.
            var expirationTime = currentTime.AddSeconds(lifeTime);

            Add(key, value, expirationTime);
        }

        /// <summary>
        /// Get value by search for specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Read(TKey key)
        {
            var actualKey = FindKey(key);

            // Key doesn't exist in pairs.
            if (!_pairs.ContainsKey(actualKey))
                return default(TValue);

            // Value found. Check its expiration time.
            var valueTimePair = _pairs[actualKey];

            // No expiration time is attached to this current value, which means it is permanent.
            if (valueTimePair.Value == null)
                return valueTimePair.Key;

            // Item is expired. Remove it from list.
            if (valueTimePair.Value < DateTime.Now)
            {
                _pairs.Remove(key);
                return default(TValue);
            }

            return valueTimePair.Key;
        }

        /// <summary>
        /// Get value by search for specific key.
        /// </summary>
        /// <returns></returns>
        public IList<TValue> ReadValues()
        {
            // Get value that aren't expired. 
            var getValuePairs = _pairs.Values.Where(x => x.Value > DateTime.Now);

            return getValuePairs.Select(x => x.Key).ToList();
        }

        /// <summary>
        /// Remove value from dictionary.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key)
        {
            var actualKey = FindKey(key);
            _pairs.Remove(actualKey);
        }


        /// <summary>
        /// Find key in dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TKey FindKey(TKey key)
        {
            return key;
        }

        #endregion
    }
}