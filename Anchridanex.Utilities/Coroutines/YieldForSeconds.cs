using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public class YieldForSeconds : IEnumerator
    {
        Stopwatch _stopwatch;
        float _seconds;

        public YieldForSeconds(float seconds)
        {
            _seconds = seconds;
            _stopwatch = new();
            _stopwatch.Start();
        }

        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            return (_stopwatch.ElapsedMilliseconds >= _seconds * 1000);
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }
    }
}
