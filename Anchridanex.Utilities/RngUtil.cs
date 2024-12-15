using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchridanex.Utilities
{
    public class RngUtil
    {
        private static Random _rng = new();

        public static void SetSeed(int seed)
        {
            _rng = new(seed);
        }

        public static int Next(int minValue, int maxValueExclusive)
        {
            return _rng.Next(minValue, maxValueExclusive);
        }

        public static int NextInclusive(int minValue, int maxValueInclusive)
        {
            return _rng.Next(minValue, maxValueInclusive + 1);
        }

        public static T? RandomItemFrom<T>(List<T> items)
        {
            return RandomItemFrom(items, false, out _);
        }

        public static T? RandomItemFrom<T>(List<T> items, bool removeFromList)
        {
            return RandomItemFrom(items, removeFromList, out _);
        }

        public static T? RandomItemFrom<T>(List<T> items, bool removeFromList, out int index)
        {
            if (HasItems(items) == false)
            {
                index = -1;
                return default(T);
            }

            int originalCount = items.Count();

            index = Next(0, originalCount);
            T? result = items.ElementAt(index);

            if (removeFromList)
                items.RemoveAt(index);

            return result;
        }

        public static T? RandomItemFrom<T>(ref T[] items)
        {
            return RandomItemFrom(ref items, false, out _);
        }

        public static T? RandomItemFrom<T>(ref T[] items, bool removeFromList)
        {
            return RandomItemFrom(ref items, removeFromList, out _);
        }

        public static T? RandomItemFrom<T>(ref T[] items, bool removeFromList, out int index)
        {
            if (HasItems(items) == false)
            {
                index = -1;
                return default(T);
            }

            int originalCount = items.Count();

            index = Next(0, originalCount);
            T? result = items[index];

            if (removeFromList)
            {
                for (int i = index + 1; i < originalCount; i++)
                    items[i - 1] = items[i];

                Array.Resize(ref items, originalCount - 1);
            }

            return result;
        }

        private static bool HasItems<T>(IEnumerable<T> items)
        {
            if (items == null)
                return false;

            if (items.Count() == 0)
                return false;

            return true;
        }

        public static T? RandomItemFrom<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            int index = Next(0, values.Length);
            return (T?)values.GetValue(index);
        }
    }
}
