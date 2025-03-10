using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public class YieldForExecutions : IEnumerator
    {
        private int _counter;
        private int _executions;

        public YieldForExecutions(int executions)
        {
            _executions = executions;
            _counter = 0;
        }

        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            _counter++;
            return _counter >= _executions;
        }

        public void Reset()
        {
            _counter = 0;
        }
    }
}
