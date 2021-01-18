namespace HackerNewsAPI.Repositories
{
    using FluentAssertions;
    using HackerNewsAPI.Common;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// NB. Third-party code (tweaked). Not fully tested
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ConcurrentFifoDictionary<TKey, TValue> : IConcurrentFifoDictionary<TKey, TValue>
    {
        #region Constructors ---------------------------------------------------------------------------------------------------------------------
        public ConcurrentFifoDictionary(IConfiguration configuration)
        {
            int.TryParse(configuration[Constants.StoriesInCacheConfigKey], out _capacity);
            _map = new Dictionary<TKey, TValue>(_capacity);
            _queue = new Queue<TKey>(_capacity);
        }
        #endregion

        #region Fields ---------------------------------------------------------------------------------------------------------------------
        private readonly int _capacity;
        private readonly Dictionary<TKey, TValue> _map;
        private readonly Queue<TKey> _queue;
        private readonly object _syncRoot = new object();
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        public bool CheckKeyExists(TKey key)
        {
            key.Should().NotBe(default(TKey));

            return _map.ContainsKey(key);
        }

        /// <summary>
        /// NB. Third-party code. Not fully tested
        /// </summary>
        public TValue GetOrAdd(TKey key, Func<TValue> createValue)
        {
            TValue value;
            // first try to get the value without locking
            // ReSharper disable once InconsistentlySynchronizedField
            if (_map.TryGetValue(key, out value))
            {
                return value;
            }

            // the value needs to be created and added
            lock (_syncRoot)
            {
                // recheck existence within critical section to avoid racing conditions
                if (_map.TryGetValue(key, out value))
                {
                    return value;
                }

                value = createValue();
                Add(key, value);
            }
            return value;
        }

        public List<TValue> GetValues(int count = int.MaxValue)
        {
            count.Should().BeGreaterThan(0);

            List<TValue> outList = new List<TValue>();
            var queueKeys = _queue.ToList();
            var mapCopy = new Dictionary<TKey, TValue>(_map);
            foreach (TKey key in queueKeys.TakeLast(count))
            {
                try
                {
                    outList.Add(mapCopy[key]);
                }
                catch (Exception ex)
                {
                    // Handle Exception
                }
            }

            return outList;
        }
        #endregion

        #region Private Methods ---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// NB. Third-party code. Not fully tested
        /// </summary>
        private void Add(TKey key, TValue value)
        {
            if (_map.Count == _capacity) // capacity reached
            {
                RemoveFirst();
            }

            _map.Add(key, value);
            _queue.Enqueue(key);
            Debug.Assert(_map.Count == _queue.Count, "The count of the queue should always equal to the count of the map.");
            Debug.Assert(_map.Count <= _capacity, "The count of items should never exceed the capacity.");
        }

        /// <summary>
        /// NB. Third-party code. Not fully tested
        /// </summary>
        private void RemoveFirst()
        {
            TKey first = _queue.Dequeue();
            _map.Remove(first);
        }
        #endregion
    }
}
