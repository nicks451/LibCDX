using System;
using System.Collections.Generic;

namespace libcdx.Utils
{
    public class Sort<T>
    {
        private static Sort<T> _instance;

        private TimSort<T> timSort;
        private ComparableTimSort comparableTimSort;

        public Sort()
        {
            
        } 

        public void Sorting(Array<T> a)
        {
            if (comparableTimSort == null)
            {
                comparableTimSort = new ComparableTimSort();
            }
            comparableTimSort.DoSort(new object[] {a.items}, 0, a.size);
        }

        public void Sorting(T[] a)
        {
            if (comparableTimSort == null)
            {
                comparableTimSort = new ComparableTimSort();
            }
            comparableTimSort.DoSort(new[] {a}, 0, a.Length);
        }

        public void Sorting(T[] a, int fromIndex, int toIndex)
        {
            if (comparableTimSort == null)
            {
                comparableTimSort = new ComparableTimSort();
            }
            comparableTimSort.DoSort(new[] {a}, fromIndex, toIndex);
        }

        public void Sorting(Array<T> a, IComparer<T> c)
        {
            if (timSort == null)
            {
                timSort = new TimSort<T>();
            }
            timSort.DoSort(a.items, c, 0, a.size);
        }

        public void Sorting(T[] a, IComparer<T> c)
        {
            if (timSort == null)
            {
                timSort = new TimSort<T>();
            }
            timSort.DoSort(a, c, 0, a.Length);
        }

        public void Sorting(T[] a, IComparer<T> c, int fromIndex, int toIndex)
        {
            if (timSort == null)
            {
                timSort = new TimSort<T>();
            }
            timSort.DoSort(a, c, fromIndex, toIndex);
        }

        /** Returns a Sort instance for convenience. Multiple threads must not use this instance at the same time. */
        public static Sort<T> Instance()
        {
            if (_instance == null)
            {
                _instance = new Sort<T>();
            }
            return _instance;
        }
    }
}