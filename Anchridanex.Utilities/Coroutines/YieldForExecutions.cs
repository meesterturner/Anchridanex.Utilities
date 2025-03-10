﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public class YieldForExecutions : YieldCondition
    {
        private int _counter;
        private int _executions;

        public YieldForExecutions(int executions, Coroutine coroutine) : base(coroutine)
        {
            _executions = executions;
            _counter = 0;
        }

        public override bool AllowRun()
        {
            _counter++;
            return _counter >= _executions;
        }
    }
}
