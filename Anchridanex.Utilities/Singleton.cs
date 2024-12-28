using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T? _instance = default(T);
        private static bool _instancing = false;

        public Singleton()
        {
            if (_instance != null)
                throw new InvalidOperationException($"Singleton {nameof(T)} already instanced");

            if (_instancing == false)
                throw new AccessViolationException($"Singleton {nameof(T)} being instance directly");
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instancing = true;
                    _instance = new T();
                    _instancing = false;
                }

                return _instance;
            }
        }
    }
}
