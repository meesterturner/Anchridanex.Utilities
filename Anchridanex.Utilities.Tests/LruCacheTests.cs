using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Tests
{
    [TestClass]
    public class LruCacheTests
    {
        [TestMethod]
        public void TestCache()
        {
            int values = 100;
            int matches = 0;

            LruCache<int, int> cache = new(values);
            for (int i = 1; i <= 100; i++)
            {
                cache[i] = Answer(i);
            }

            for (int i = 1; i <= 100; i++)
            {
                int fromCache = cache[i];
                int expected = Answer(i);
                if (fromCache != expected)
                    Assert.Fail($"Value {i} returned {fromCache} instead of {expected}");
                else
                    matches++;
            }

            Assert.AreEqual(values, matches);
        }

        private int Answer(int i)
        {
            return i * 79;
        }

    }
}
