using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Tests
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void TestDirectInstancing()
        {
            Exception x = new AccessViolationException();
            Assert.ThrowsException<AccessViolationException>(() => { TestSingletonInt newInstance = new(); });
        }

        [TestMethod]
        public void TestDuplicateInstancing()
        {
            TestSingletonInt.Instance.Value = 123;
            TestSingletonInt.Instance.Value = 789;
            Assert.ThrowsException<InvalidOperationException>(() => { TestSingletonInt newInstance = new(); });
        }

        [TestMethod]
        public void TestSingletonInstancing()
        {
            Random rng = new();
            int randomValue = rng.Next(0, 100);

            TestSingletonInt.Instance.Value = randomValue;

            Assert.IsTrue(TestSingletonInt.Instance.Value == randomValue);
        }

        [TestMethod]
        public void TestMultipleDistinctSingletons()
        {
            const int INT_VALUE = 1979;
            const string STRING_VALUE = "Words";

            TestSingletonInt.Instance.Value = INT_VALUE;
            TestSingletonString.Instance.Value = STRING_VALUE;

            Assert.IsTrue(TestSingletonInt.Instance.Value == INT_VALUE &&
                TestSingletonString.Instance.Value == STRING_VALUE);
        }


    }

    public class TestSingletonInt : Singleton<TestSingletonInt>
    {
        public int Value { get; set; }
    }

    public class TestSingletonString : Singleton<TestSingletonString>
    {
        public string Value { get; set; } = string.Empty;
    }
}
