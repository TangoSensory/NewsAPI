namespace HackerNewsAPI.Repositories
{
    using System; 
    using System.Collections.Generic;
    using System.Linq;
    public interface IConcurrentFifoDictionary<TKey, TValue>
    {
        bool CheckKeyExists(TKey key);
        TValue GetOrAdd(TKey key, Func<TValue> createValue);
        List<TValue> GetValues(int count = -1);
    }
}
