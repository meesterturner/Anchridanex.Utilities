using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities
{
    public class LruCache<TKey, TValue> where TKey : notnull
    {
        private const int DEFAULT_CAPACITY = 100;
        private int _capacity;
        private Queue<TKey> keyQueue;
        private Dictionary<TKey, TValue?> cacheDict;

        public LruCache()
        {
            _capacity = DEFAULT_CAPACITY;
            keyQueue = new(_capacity);
            cacheDict = new(_capacity);
        }

        public LruCache(int capacity)
        {
            _capacity = capacity;
            keyQueue = new(_capacity);
            cacheDict = new(_capacity);
        }

        public TValue? this[TKey key]
        {
            get
            {
                TValue? value;
                bool success = TryGetValue(key, out value);

                if (success)
                    return value;

                return default(TValue);
            }

            set
            {
                Add(key, value);
            }
        }

        public void Add(TKey key, TValue? value)
        {
            if (keyQueue.Count == _capacity)
            {
                TKey oldest = keyQueue.Dequeue();
                cacheDict.Remove(oldest);
            }

            cacheDict[key] = value;
            keyQueue.Enqueue(key);
        }

        public bool TryGetValue(TKey key, out TValue? value)
        {
            return cacheDict.TryGetValue(key, out value);
        }
    }
}
