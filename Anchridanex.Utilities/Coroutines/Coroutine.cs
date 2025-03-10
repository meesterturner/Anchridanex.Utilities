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
        private Func<IEnumerator>? _coroutine;
        private IEnumerator? _yieldCondition;
        private bool _isStarted = false;

        /// <summary>
        /// Starts the given coroutine
        /// </summary>
        /// <param name="coroutine">Coroutine to run</param>
        public void Start(Func<IEnumerator> coroutine)
        {
            if(coroutine is null)
                throw new ArgumentNullException(nameof(coroutine));

            _coroutine = coroutine;
            _yieldCondition = null;
            _isStarted = true;
            Execute();
        }

        /// <summary>
        /// Will execute the currently managed coroutine if the condition to do so is correct. 
        /// This method should be called on a regular basis. E.g., in a game context, every frame.
        /// </summary>
        public void Execute()
        {
            if (_isStarted is false || _coroutine is null)
                return;

            bool runNext = false;

            if (_yieldCondition is null)
                runNext = true;

            else
                runNext = _yieldCondition.MoveNext();

            if (runNext)
                _yieldCondition = _coroutine.Invoke();
        }

        /// <summary>
        /// Stops the current coroutine. Has no effect if this object is currently not managing a coroutine
        /// </summary>
        public void Stop()
        {
            _isStarted = false;
            _coroutine = null;
            _yieldCondition = null;
        }
    }
}
