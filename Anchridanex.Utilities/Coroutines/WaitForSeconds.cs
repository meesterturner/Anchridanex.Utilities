using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public class WaitForSeconds : YieldConditionBase
    {
        Stopwatch _stopwatch;
        float _seconds;

        public WaitForSeconds(float seconds, Coroutine coroutine) : base(coroutine)
        {
            _seconds = seconds;
            _stopwatch = new();
            _stopwatch.Start();
        }

        public override bool AllowRun()
        {
            return (_stopwatch.ElapsedMilliseconds >= _seconds * 1000);
        }
    }
}
