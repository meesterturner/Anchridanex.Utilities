using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Tests
{
    [TestClass]
    public class RandomItemTests
    {
        private enum TestEnum
        {
            None,
            One,
            Two,
            Three
        }

        private List<string> DefaultItems()
        {
            return new List<string>() {
                "one", "two", "three"
            };
        }

        [TestMethod]
        public void TestClassInstance()
        {
            RngUtil rng = new();
            int num = rng.NextInclusive(1, 10);
            Assert.IsTrue(num >= 1 && num <= 10);
        }

        [TestMethod]
        public void TestRandomItemFromList()
        {
            RngUtil rng = new();
            int randomIndex;

            List<string> items = DefaultItems();
            string? randomString = rng.RandomItemFrom(items, false, out randomIndex);

            Assert.IsTrue(
                randomString != null &&
                randomIndex >= 0 &&
                randomIndex < DefaultItems().Count
                );
            
        }

        [TestMethod]
        public void TestAndRemoveRandomItemFromList()
        {
            RngUtil rng = new();
            List<string> items = DefaultItems();
            int originalCount = items.Count;
            string? randomString = rng.RandomItemFrom(items, true);

            Assert.IsTrue(
                items.Count == originalCount - 1 &&
                randomString != null &&
                items.Contains(randomString) == false
                );
        }

        [TestMethod]
        public void TestAndRemoveRandomItemFromArray()
        {
            RngUtil rng = new();
            string[] items = DefaultItems().ToArray();
            int originalCount = items.Length;
            string? randomString = rng.RandomItemFrom(ref items, true, out _);
            
            Assert.IsTrue(
                items.Length == originalCount - 1 &&
                randomString != null &&
                items.Contains(randomString) == false
                );
        }

        [TestMethod]
        public void TestRandomEnum()
        {
            RngUtil rng = new();
            TestEnum test = rng.RandomItemFrom<TestEnum>();
            Assert.IsTrue(
                (int)test >= 0 && (int)test <= 3
                );
        }

        [TestMethod]
        public void ProveListRemovalIsByReference()
        {
            List<string> items = DefaultItems();
            ProveListRemovalIsByReference_Action<string>(items, out _);
            Assert.AreEqual(items.Count, 2);
        }

        private void ProveListRemovalIsByReference_Action<T>(IEnumerable<T> items, out int index)
        {
            index = 1;
            List<T> tempList = (List<T>)items;
            tempList.RemoveAt(index);
            items = tempList;

        }
    }
}
