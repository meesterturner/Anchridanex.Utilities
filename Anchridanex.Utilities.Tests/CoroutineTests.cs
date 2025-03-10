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

        private IEnumerator YieldForSecondsTest(Coroutine c)
        {
            Debug.WriteLine("Started");
            c.YieldCondition = new YieldForSeconds(SECONDS);
            yield return null;
            _yieldForSecondsComplete = true;
        }

        [TestMethod]
        public void TestYieldForExecutions()
        {
            Coroutine co = new();
            co.Start(YieldForExecutionsTest);

            int execs = -1; // First execute is to perform the action
                            // we only want to measure those after the first execute

            while (execs < 10 && _yieldForExecutionsComplete is false)
            {
                execs++;
                co.Execute();
            }

            Assert.IsTrue(execs == EXECUTIONS && _yieldForExecutionsComplete is true);
        }

        private IEnumerator YieldForExecutionsTest(Coroutine c)
        {
            Debug.WriteLine("Started");
            c.YieldCondition = new YieldForExecutions(EXECUTIONS);
            yield return null;
            _yieldForExecutionsComplete = true;
        }
    }
}
