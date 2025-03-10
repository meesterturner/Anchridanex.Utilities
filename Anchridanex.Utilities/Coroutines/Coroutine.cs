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
        private YieldConditionBase? _yieldCondition;
        private IEnumerator? _coroutine;
        private bool _isStarted = false;

        /// <summary>
        /// Creates a new Coroutine object with the provided coroutine to manage
        /// </summary>
        /// <param name="coroutine">Coroutine to run</param>
        /// <returns>Coroutine object</returns>
        public static Coroutine CreateAndStart(Func<Coroutine, IEnumerator> coroutine)
        {
            Coroutine newCoroutine = new();
            newCoroutine.Start(coroutine);
            return newCoroutine;
        }

        /// <summary>
        /// Starts the given coroutine
        /// </summary>
        /// <param name="coroutine">Coroutine to run</param>
        public void Start(Func<Coroutine, IEnumerator> coroutine)
        {
            if(coroutine is null)
                throw new ArgumentNullException(nameof(coroutine));

            _coroutine = coroutine(this);
            _yieldCondition = null;
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

            if (_yieldCondition is not null)
            {
                if (_yieldCondition.AllowRun() is false)
                    return;

                _yieldCondition = null;
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
            _yieldCondition = null;
        }

        /// <summary>
        /// Sets the yield conditon
        /// </summary>
        /// <param name="condition">A new IYieldCondition object</param>>
        public void SetYield(YieldConditionBase condition)
        {
            _yieldCondition = condition;
        }
    }
}
