using System.Collections.Generic;

namespace libcdx.Utils
{
    public class QuickSelect<T>
    {
        private T[] array;
        private IComparer<T> comp;

        public int Select(T[] items, IComparer<T> comp, int n, int size)
        {
            this.array = items;
            this.comp = comp;
            return RecursiveSelect(0, size - 1, n);
        }

        private int Partition(int left, int right, int pivot)
        {
            T pivotValue = array[pivot];
            Swap(right, pivot);
            int storage = left;
            for (int i = left; i < right; i++)
            {
                if (comp.Compare(array[i], pivotValue) < 0)
                {
                    Swap(storage, i);
                    storage++;
                }
            }
            Swap(right, storage);
            return storage;
        }

        private int RecursiveSelect(int left, int right, int k)
        {
            if (left == right) return left;
            int pivotIndex = MedianOfThreePivot(left, right);
            int pivotNewIndex = Partition(left, right, pivotIndex);
            int pivotDist = (pivotNewIndex - left) + 1;
            int result;
            if (pivotDist == k)
            {
                result = pivotNewIndex;
            }
            else if (k < pivotDist)
            {
                result = RecursiveSelect(left, pivotNewIndex - 1, k);
            }
            else
            {
                result = RecursiveSelect(pivotNewIndex + 1, right, k - pivotDist);
            }
            return result;
        }

        /** Median of Three has the potential to outperform a random pivot, especially for partially sorted arrays */
        private int MedianOfThreePivot(int leftIdx, int rightIdx)
        {
            T left = array[leftIdx];
            int midIdx = (leftIdx + rightIdx) / 2;
            T mid = array[midIdx];
            T right = array[rightIdx];

            // spaghetti median of three algorithm
            // does at most 3 comparisons
            if (comp.Compare(left, mid) > 0)
            {
                if (comp.Compare(mid, right) > 0)
                {
                    return midIdx;
                }
                else if (comp.Compare(left, right) > 0)
                {
                    return rightIdx;
                }
                else
                {
                    return leftIdx;
                }
            }
            else
            {
                if (comp.Compare(left, right) > 0)
                {
                    return leftIdx;
                }
                else if (comp.Compare(mid, right) > 0)
                {
                    return rightIdx;
                }
                else
                {
                    return midIdx;
                }
            }
        }

        private void Swap(int left, int right)
        {
            T tmp = array[left];
            array[left] = array[right];
            array[right] = tmp;
        }
    }
}