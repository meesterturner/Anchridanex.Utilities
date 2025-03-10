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

        DateTime waitUntilTime;

        bool _yieldForSecondsComplete = false;
        bool _yieldForExecutionsComplete = false;
        bool _yieldForTimeComplete = false;

        [TestMethod]
        public void TestWaitForSeconds()
        {
            Coroutine co = Coroutine.CreateAndStart(WaitForSecondsTest);
            Stopwatch sw = new();
            sw.Start();

            while (sw.ElapsedMilliseconds < SECONDS * 2000 && _yieldForSecondsComplete is false)
            {
                co.Execute();
            }

            sw.Stop();

            Assert.IsTrue(_yieldForSecondsComplete is true && sw.ElapsedMilliseconds < ((SECONDS + 0.2) * 1000));
        }

        private IEnumerator WaitForSecondsTest(Coroutine c)
        {
            Debug.WriteLine("Started");
            yield return new WaitForSeconds(SECONDS, c);
            _yieldForSecondsComplete = true;
        }

        [TestMethod]
        public void TestWaitForExecutions()
        {
            Coroutine co = Coroutine.CreateAndStart(WaitForExecutionsTest);

            int execs = -1; // First execute is to perform the action
                            // we only want to measure those after the first execute

            while (execs < 10 && _yieldForExecutionsComplete is false)
            {
                execs++;
                co.Execute();
            }

            Assert.IsTrue(execs == EXECUTIONS && _yieldForExecutionsComplete is true);
        }

        private IEnumerator WaitForExecutionsTest(Coroutine c)
        {
            Debug.WriteLine("Started");
            yield return new WaitForExecutions(EXECUTIONS, c);
            _yieldForExecutionsComplete = true;
        }

        [TestMethod]
        public void TestWaitForTime()
        {

            waitUntilTime = DateTime.Now.AddSeconds(3);
            DateTime maximumWaitTime = DateTime.Now.AddSeconds(10);

            Coroutine co = Coroutine.CreateAndStart(WaitUntilTimeTest);

            while (DateTime.Now < maximumWaitTime && _yieldForTimeComplete is false)
            {
                co.Execute();
            }

            Assert.IsTrue(DateTime.Now >= waitUntilTime && DateTime.Now <= waitUntilTime.AddMilliseconds(250) && _yieldForTimeComplete is true);
        }

        private IEnumerator WaitUntilTimeTest(Coroutine c)
        {
            Debug.WriteLine("Started WaitForTimeTest()");
            yield return new WaitUntilTime(waitUntilTime, c);
            _yieldForTimeComplete = true;
        }
    }
}
