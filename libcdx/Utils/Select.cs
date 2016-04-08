using System.Collections.Generic;

namespace libcdx.Utils
{
    public class Select<T>
    {
        private static Select<T> _instance;
        private QuickSelect<T> quickSelect;

        /** Provided for convenience */
        public static Select<T> instance()
        {
            if (_instance == null)
            {
                _instance = new Select<T>();
            }
            return _instance;
        }

        public T Selecting(T[] items, IComparer<T> comp, int kthLowest, int size)
        {
            int idx = SelectIndex(items, comp, kthLowest, size);
            return items[idx];
        }

        public int SelectIndex(T[] items, IComparer<T> comp, int kthLowest, int size)
        {
            if (size < 1)
            {
                throw new CdxRuntimeException("cannot select from empty array (size < 1)");
            }
            else if (kthLowest > size)
            {
                throw new CdxRuntimeException("Kth rank is larger than size. k: " + kthLowest + ", size: " + size);
            }
            int idx;
            // naive partial selection sort almost certain to outperform quickselect where n is min or max
            if (kthLowest == 1)
            {
                // find min
                idx = FastMin(items, comp, size);
            }
            else if (kthLowest == size)
            {
                // find max
                idx = FastMax(items, comp, size);
            }
            else
            {
                // quickselect a better choice for cases of k between min and max
                if (quickSelect == null) quickSelect = new QuickSelect<T>();
                idx = quickSelect.Select(items, comp, kthLowest, size);
            }
            return idx;
        }

        /** Faster than quickselect for n = min */
        private int FastMin(T[] items, IComparer<T> comp, int size)
        {
            int lowestIdx = 0;
            for (int i = 1; i < size; i++)
            {
                int comparison = comp.Compare(items[i], items[lowestIdx]);
                if (comparison < 0)
                {
                    lowestIdx = i;
                }
            }
            return lowestIdx;
        }

        /** Faster than quickselect for n = max */
        private int FastMax(T[] items, IComparer<T> comp, int size)
        {
            int highestIdx = 0;
            for (int i = 1; i < size; i++)
            {
                int comparison = comp.Compare(items[i], items[highestIdx]);
                if (comparison > 0)
                {
                    highestIdx = i;
                }
            }
            return highestIdx;
        }
    }
}