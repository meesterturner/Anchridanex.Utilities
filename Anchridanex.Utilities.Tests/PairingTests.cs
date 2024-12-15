namespace Anchridanex.Utilities.Tests
{
    [TestClass]
    public class PairingTests
    {
        private class PairTestObject
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; } = 0;

            public PairTestObject(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        PairTestObject obj1;
        PairTestObject obj2;
        PairTestObject obj3;

        Pairing<PairTestObject> completePair;
        Pairing<PairTestObject> incompletePair;

        public PairingTests() 
        {
            obj1 = new("London", 0);
            obj2 = new("Edinburgh", 396);
            obj3 = new("Grimsby", 220);

            completePair = new();
            completePair.First = obj1;
            completePair.Second = obj2;

            incompletePair = new();
            incompletePair.Second = obj3;
        }

        [TestMethod]
        public void TestCompletePairExactPositiveMatch()
        {
            Assert.IsTrue(completePair.MatchesInExactOrder(obj1, obj2));
        }

        [TestMethod]
        public void TestCompletePairExactNegativeMatch()
        {
            Assert.IsFalse(completePair.MatchesInExactOrder(obj2, obj1));
        }

        [TestMethod]
        public void TestCompletePairEitherPositiveMatch()
        {
            Assert.IsTrue(completePair.MatchesInEitherOrder(obj2, obj1));
        }

        [TestMethod]
        public void TestCompletePairEitherNegativeMatch()
        {
            Assert.IsFalse(completePair.MatchesInEitherOrder(obj2, obj3));
        }

        [TestMethod]
        public void TestCompletePairContainsPositiveMatch()
        {
            Assert.IsTrue(completePair.Contains(obj2) && completePair.Contains(obj1));
        }

        [TestMethod]
        public void TestCompletePairContainsNegativeMatch()
        {
            Assert.IsFalse(completePair.Contains(obj3));
        }

        [TestMethod]
        public void TestIncompletePairContainsObjectPositiveMatch()
        {
            Assert.IsTrue(incompletePair.Contains(obj3));
        }

        [TestMethod]
        public void TestIncompletePairContainsNullPositiveMatch()
        {
            Assert.IsTrue(incompletePair.Contains(null));
        }

        [TestMethod]
        public void TestIncompletePairContainsObjectNegativeMatch()
        {
            Assert.IsFalse(incompletePair.Contains(obj1));
        }
    }
}