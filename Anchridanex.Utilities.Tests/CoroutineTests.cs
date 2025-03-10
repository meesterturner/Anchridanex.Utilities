using Anchridanex.Utilities.Coroutines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Tests
{
    [TestClass]
    public class CoroutineTests
    {
        const float SECONDS = 4.5f;
        const int EXECUTIONS = 3;

        bool _yieldForSecondsComplete = false;
        bool _yieldForExecutionsComplete = false;

        [TestMethod]
        public void TestYieldForSeconds()
        {
            Coroutine co = new();
            Stopwatch sw = new();
            sw.Start();
            co.Start(YieldForSecondsTest);

            while (sw.ElapsedMilliseconds < SECONDS * 2000 && _yieldForSecondsComplete is false)
            {
                co.Execute();
            }

            sw.Stop();

            Assert.IsTrue(_yieldForSecondsComplete is true && sw.ElapsedMilliseconds < ((SECONDS + 0.2) * 1000));
        }

        private IEnumerator YieldForSecondsTest()
        {
            Debug.WriteLine("Started");
            yield return new YieldForSeconds(SECONDS);
            _yieldForSecondsComplete = true;
        }

        [TestMethod]
        public void TestYieldForExecutions()
        {
            Coroutine co = new();
            co.Start(YieldForExecutionsTest);

            int execs = 0;

            while (execs < 10 && _yieldForExecutionsComplete is false)
            {
                execs++;
                co.Execute();
            }

            Assert.IsTrue(execs == EXECUTIONS && _yieldForExecutionsComplete is true);
        }

        private IEnumerator YieldForExecutionsTest()
        {
            Debug.WriteLine("Started");
            yield return new YieldForExecutions(EXECUTIONS);
            _yieldForExecutionsComplete = true;
        }
    }
}
