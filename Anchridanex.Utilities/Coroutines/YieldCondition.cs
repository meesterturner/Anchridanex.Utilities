using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public abstract class YieldCondition
    {
        public YieldCondition(Coroutine coroutine)
        {
            coroutine.SetYield(this);
        }

        public abstract bool AllowRun();
    }
}
