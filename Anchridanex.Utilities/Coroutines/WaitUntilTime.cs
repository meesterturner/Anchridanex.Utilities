using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public class WaitUntilTime : YieldConditionBase
    {
        private DateTime _waitTime;

        public WaitUntilTime(DateTime time, Coroutine coroutine) : base(coroutine)
        {
            if (time < DateTime.Now)
                throw new ArgumentOutOfRangeException($"{nameof(time)} cannot be in the past");

            _waitTime = time;
        }

        public override bool AllowRun()
        {
            return DateTime.Now >= _waitTime;
        }
    }
}
