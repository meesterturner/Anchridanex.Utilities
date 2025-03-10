using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities.Coroutines
{
    public class Coroutine
    {
        private IYieldCondition? _yieldCondition;
        public IYieldCondition? YieldCondition
        {
            get
            {
                return _yieldCondition;
            }
            set
            {
                _yieldCondition = value;
            }
        }

        private IEnumerator? _coroutine;
        private bool _isStarted = false;

        /// <summary>
        /// Starts the given coroutine
        /// </summary>
        /// <param name="coroutine">Coroutine to run</param>
        public void Start(Func<Coroutine, IEnumerator> coroutine)
        {
            if(coroutine is null)
                throw new ArgumentNullException(nameof(coroutine));

            _coroutine = coroutine(this);
            YieldCondition = null;
            _isStarted = true;
        }

        /// <summary>
        /// Will execute the currently managed coroutine if the condition to do so is correct. 
        /// This method should be called on a regular basis. E.g., in a game context, every frame.
        /// </summary>
        public void Execute()
        {
            if (_coroutine is null)
                return;

            if (YieldCondition is not null)
            {
                if (YieldCondition.AllowRun() is false)
                    return;

                YieldCondition = null;
            }

            if (_coroutine.MoveNext() is false)
                _coroutine = null; // Coroutine completed
        }

        /// <summary>
        /// Stops the current coroutine. Has no effect if this object is currently not managing a coroutine
        /// </summary>
        public void Stop()
        {
            _isStarted = false;
            _coroutine = null;
            YieldCondition = null;
        }
    }
}
