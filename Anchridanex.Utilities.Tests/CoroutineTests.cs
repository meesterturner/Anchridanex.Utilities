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
            Coroutine co = Coroutine.CreateAndStart(YieldForSecondsTest);
            Stopwatch sw = new();
            sw.Start();

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
            yield return c.SetYield(new YieldForSeconds(SECONDS));
            _yieldForSecondsComplete = true;
        }

        [TestMethod]
        public void TestYieldForExecutions()
        {
            Coroutine co = Coroutine.CreateAndStart(YieldForExecutionsTest);

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
            yield return c.SetYield(new YieldForExecutions(EXECUTIONS));
            _yieldForExecutionsComplete = true;
        }
    }
}
