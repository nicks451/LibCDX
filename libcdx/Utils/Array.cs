using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using libcdx.Math;

namespace libcdx.Utils
{
    public class Array<T> : IEnumerable<T>
    {
        /** Provides direct access to the underlying array. If the Array's generic type is not Object, this field may only be accessed
      * if the {@link Array#Array(bool, int, Class)} constructor was used. */
        public T[] items;

        public int size;
        public bool ordered;

        private ArrayIterable<T> iterable;
        private PredicateIterable<T> predicateIterable;

        /** Creates an ordered array with a capacity of 16. */
        public Array() : this(true, 16)
        {
        }

        /** Creates an ordered array with the specified capacity. */
        public Array(int capacity) : this(true, capacity)
        {
        }

        /** @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
         *           memory copy.
         * @param capacity Any elements added beyond this will cause the backing array to be grown. */
        public Array(bool ordered, int capacity)
        {
            this.ordered = ordered;
            items = new T[capacity];
        }

        /** Creates a new array with {@link #items} of the specified type.
         * @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
         *           memory copy.
         * @param capacity Any elements added beyond this will cause the backing array to be grown. */
        public Array(bool ordered, int capacity, Type arrayType)
        {
            this.ordered = ordered;
            items = (T[])ArrayReflection.NewInstance(arrayType, capacity);
        }

        /** Creates an ordered array with {@link #items} of the specified type and a capacity of 16. */
        public Array(Type arrayType) : this(true, 16, arrayType)
        {
        }

        /** Creates a new array containing the elements in the specified array. The new array will have the same type of backing array
         * and will be ordered if the specified array is ordered. The capacity is set to the number of elements, so any subsequent
         * elements added will cause the backing array to be grown. */
        public Array(Array<T> array) : this(array.ordered, array.size, array.items.GetType().BaseType)
        {
            size = array.size;
            Array.Copy(array.items, 0, items, 0, size);
        }

        /** Creates a new ordered array containing the elements in the specified array. The new array will have the same type of
         * backing array. The capacity is set to the number of elements, so any subsequent elements added will cause the backing array
         * to be grown. */
        public Array(T[] array) : this(true, array, 0, array.Length)
        {
        }

        /** Creates a new array containing the elements in the specified array. The new array will have the same type of backing array.
         * The capacity is set to the number of elements, so any subsequent elements added will cause the backing array to be grown.
         * @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
         *           memory copy. */
        public Array(bool ordered, T[] array, int start, int count) : this(ordered, count, (Type)array.GetType().BaseType)
        {
            size = count;
            Array.Copy(array, start, items, 0, size);
        }

        public void Add(T value)
        {
            T[] items = this.items;
            if (size == items.Length)
            {
                items = Resize(System.Math.Max(8, (int)(size * 1.75f)));
            }
            items[size++] = value;
        }

        public void addAll(Array<T> array)
        {
            AddAll(array, 0, array.size);
        }

        public void AddAll(Array<T> array, int start, int count)
        {
            if (start + count > array.size)
            {
                throw new ArgumentOutOfRangeException("start + count must be <= size: " + start + " + " + count + " <= " + array.size);
            }
            AddAll((T[])array.items, start, count);
        }

        public void addAll(params T[] array)
        {
            AddAll(array, 0, array.Length);
        }

        public void AddAll(T[] array, int start, int count)
        {
            T[] items = this.items;
            int sizeNeeded = size + count;
            if (sizeNeeded > items.Length)
            {
                items = Resize(System.Math.Max(8, (int)(sizeNeeded * 1.75f)));
            }
            Array.Copy(array, start, items, size, count);
            size += count;
        }

        public T Get(int index)
        {
            if (index >= size)
            {
                throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            }
            return items[index];
        }

        public void Set(int index, T value)
        {
            if (index >= size)
            {
                throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            }
            items[index] = value;
        }

        public void Insert(int index, T value)
        {
            if (index > size)
            {
                throw new ArgumentOutOfRangeException("index can't be > size: " + index + " > " + size);
            }
            T[] items = this.items;
            if (size == items.Length)
            {
                items = Resize(System.Math.Max(8, (int)(size * 1.75f)));
            }
            if (ordered)
            {
                Array.Copy(items, index, items, index + 1, size - index);
            }
            else
            {
                items[size] = items[index];
            }
            size++;
            items[index] = value;
        }

        public void Swap(int first, int second)
        {
            if (first >= size)
            {
                throw new ArgumentOutOfRangeException("first can't be >= size: " + first + " >= " + size);
            }
            if (second >= size)
            {
                throw new ArgumentOutOfRangeException("second can't be >= size: " + second + " >= " + size);
            }
            T[] items = this.items;
            T firstValue = items[first];
            items[first] = items[second];
            items[second] = firstValue;
        }

        /** Returns if this array contains value.
         * @param value May be null.
         * @param identity If true, == comparison will be used. If false, .equals() comparison will be used.
         * @return true if array contains value, false if it doesn't */
        public bool Contains(T value, bool identity)
        {
            T[] items = this.items;
            int i = size - 1;
            if (identity || value == null)
            {
                while (i >= 0)
                    if (items[i--].Equals(value))
                    {
                        return true;
                    }
            }
            else
            {
                while (i >= 0)
                {
                    if (value.Equals(items[i--]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /** Returns the index of first occurrence of value in the array, or -1 if no such value exists.
         * @param value May be null.
         * @param identity If true, == comparison will be used. If false, .equals() comparison will be used.
         * @return An index of first occurrence of value in array or -1 if no such value exists */
        public int IndexOf(T value, bool identity)
        {
            T[] items = this.items;
            if (identity || value == null)
            {
                for (int i = 0, n = size; i < n; i++)
                    if (items[i].Equals(value))
                    {
                        return i;
                    }
            }
            else
            {
                for (int i = 0, n = size; i < n; i++)
                {
                    if (value.Equals(items[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /** Returns an index of last occurrence of value in array or -1 if no such value exists. Search is started from the end of an
         * array.
         * @param value May be null.
         * @param identity If true, == comparison will be used. If false, .equals() comparison will be used.
         * @return An index of last occurrence of value in array or -1 if no such value exists */
        public int LastIndexOf(T value, bool identity)
        {
            T[] items = this.items;
            if (identity || value == null)
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    if (items[i].Equals(value))
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = size - 1; i >= 0; i--)
                    if (value.Equals(items[i]))
                    {
                        return i;
                    }
            }
            return -1;
        }

        /** Removes the first instance of the specified value in the array.
         * @param value May be null.
         * @param identity If true, == comparison will be used. If false, .equals() comparison will be used.
         * @return true if value was found and removed, false otherwise */
        public bool RemoveValue(T value, bool identity)
        {
            T[] items = this.items;
            if (identity || value == null)
            {
                for (int i = 0, n = size; i < n; i++)
                {
                    if (items[i].Equals(value))
                    {
                        RemoveIndex(i);
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0, n = size; i < n; i++)
                {
                    if (value.Equals(items[i]))
                    {
                        RemoveIndex(i);
                        return true;
                    }
                }
            }
            return false;
        }

        /** Removes and returns the item at the specified index. */
        public T RemoveIndex(int index)
        {
            if (index >= size)
            {
                throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            }
            T[] items = this.items;
            T value = (T)items[index];
            size--;
            if (ordered)
            {
                Array.Copy(items, index + 1, items, index, size - index);
            }
            else
            {
                items[index] = items[size];
            }
            items[size] = default(T);
            return value;
        }

        /** Removes the items between the specified indices, inclusive. */
        public void RemoveRange(int start, int end)
        {
            if (end >= size)
            {
                throw new ArgumentOutOfRangeException("end can't be >= size: " + end + " >= " + size);
            }
            if (start > end)
            {
                throw new ArgumentOutOfRangeException("start can't be > end: " + start + " > " + end);
            }
            T[] items = this.items;
            int count = end - start + 1;
            if (ordered)
            {
                Array.Copy(items, start + count, items, start, size - (start + count));
            }
            else
            {
                int lastIndex = this.size - 1;
                for (int i = 0; i < count; i++)
                {
                    items[start + i] = items[lastIndex - i];
                }
            }
            size -= count;
        }

        /** Removes from this array all of elements contained in the specified array.
         * @param identity True to use ==, false to use .equals().
         * @return true if this array was modified. */
        public bool RemoveAll(Array<T> array, bool identity)
        {
            int size = this.size;
            int startSize = size;
            T[] items = this.items;
            if (identity)
            {
                for (int i = 0, n = array.size; i < n; i++)
                {
                    T item = array.Get(i);
                    for (int ii = 0; ii < size; ii++)
                    {
                        if (item.Equals(items[ii]))
                        {
                            RemoveIndex(ii);
                            size--;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0, n = array.size; i < n; i++)
                {
                    T item = array.Get(i);
                    for (int ii = 0; ii < size; ii++)
                    {
                        if (item.Equals(items[ii]))
                        {
                            RemoveIndex(ii);
                            size--;
                            break;
                        }
                    }
                }
            }
            return size != startSize;
        }

        /** Removes and returns the last item. */
        public T Pop()
        {
            if (size == 0)
            {
                throw new IndexOutOfRangeException("Array is empty.");
            }
            --size;
            T item = items[size];
            items[size] = default(T);
            return item;
        }

        /** Returns the last item. */
        public T Peek()
        {
            if (size == 0)
            {
                throw new IndexOutOfRangeException("Array is empty.");
            }
            return items[size - 1];
        }

        /** Returns the first item. */
        public T First()
        {
            if (size == 0)
            {
                throw new IndexOutOfRangeException("Array is empty.");
            }
            return items[0];
        }

        public void Clear()
        {
            T[] items = this.items;
            for (int i = 0, n = size; i < n; i++)
            {
                items[i] = default(T);
            }
            size = 0;
        }

        /** Reduces the size of the backing array to the size of the actual items. This is useful to release memory when many items
         * have been removed, or if it is known that more items will not be added.
         * @return {@link #items} */
        public T[] Shrink()
        {
            if (items.Length != size)
            {
                Resize(size);
            }
            return items;
        }

        /** Increases the size of the backing array to accommodate the specified number of additional items. Useful before adding many
         * items to avoid multiple backing array resizes.
         * @return {@link #items} */
        public T[] EnsureCapacity(int additionalCapacity)
        {
            int sizeNeeded = size + additionalCapacity;
            if (sizeNeeded > items.Length)
            {
                Resize(System.Math.Max(8, sizeNeeded));
            }
            return items;
        }

        /** Creates a new backing array with the specified size containing the current items. */
        protected T[] Resize(int newSize)
        {
            T[] items = this.items;
            T[] newItems = (T[])ArrayReflection.NewInstance(items.GetType().BaseType, newSize);
            Array.Copy(items, 0, newItems, 0, System.Math.Min(size, newItems.Length));
            this.items = newItems;
            return newItems;
        }

        /** Sorts this array. The array elements must implement {@link Comparable}. This method is not thread safe (uses
         * {@link Sort#instance()}). */
        public void Sort()
        {
            Sort<T>.Instance().Sorting(items, 0, size);
        }

        /** Sorts the array. This method is not thread safe (uses {@link Sort#instance()}). */
        public void Sort(IComparer<T> comparator)
        {
            Sort<T>.Instance().Sorting(items, comparator, 0, size);
        }

        /** Selects the nth-lowest element from the Array according to Comparator ranking. This might partially sort the Array. The
         * array must have a size greater than 0, or a {@link com.badlogic.gdx.utils.GdxRuntimeException} will be thrown.
         * @see Select
         * @param comparator used for comparison
         * @param kthLowest rank of desired object according to comparison, n is based on ordinal numbers, not array indices. for min
         *           value use 1, for max value use size of array, using 0 results in runtime exception.
         * @return the value of the Nth lowest ranked object. */
        public T SelectRanked(IComparer<T> comparator, int kthLowest)
        {
            if (kthLowest < 1)
            {
                throw new CdxRuntimeException("nth_lowest must be greater than 0, 1 = first, 2 = second...");
            }
            return Select<T>.instance().Selecting(items, comparator, kthLowest, size);
        }

        /** @see Array#selectRanked(java.util.Comparator, int)
         * @param comparator used for comparison
         * @param kthLowest rank of desired object according to comparison, n is based on ordinal numbers, not array indices. for min
         *           value use 1, for max value use size of array, using 0 results in runtime exception.
         * @return the index of the Nth lowest ranked object. */
        public int SelectRankedIndex(IComparer<T> comparator, int kthLowest)
        {
            if (kthLowest < 1)
            {
                throw new CdxRuntimeException("nth_lowest must be greater than 0, 1 = first, 2 = second...");
            }
            return Select<T>.instance().SelectIndex(items, comparator, kthLowest, size);
        }

        public void Reverse()
        {
            T[] items = this.items;
            for (int i = 0, lastIndex = size - 1, n = size / 2; i < n; i++)
            {
                int ii = lastIndex - i;
                T temp = items[i];
                items[i] = items[ii];
                items[ii] = temp;
            }
        }

        public void Shuffle()
        {
            T[] items = this.items;
            for (int i = size - 1; i >= 0; i--)
            {
                int ii = MathUtils.Random(i);
                T temp = items[i];
                items[i] = items[ii];
                items[ii] = temp;
            }
        }

        /** Returns an iterator for the items in the array. Remove is supported. Note that the same iterator instance is returned each
         * time this method is called. Use the {@link ArrayIterator} constructor for nested or multithreaded iteration. */
        public IEnumerator<T> Iterator()
        {
            if (iterable == null) iterable = new ArrayIterable<T>(this);
            return iterable.Iterator();
        }

        /** Returns an iterable for the selected items in the array. Remove is supported, but not between hasNext() and next(). Note
         * that the same iterable instance is returned each time this method is called. Use the {@link Predicate.PredicateIterable}
         * constructor for nested or multithreaded iteration. */
        public IEnumerable<T> Select(IPredicate<T> predicate)
        {
            if (predicateIterable == null)
            {
                predicateIterable = new PredicateIterable<T>(this, predicate);
            }
            else
            {
                predicateIterable.Set(this, predicate);
            }
            return predicateIterable;
        }

        /** Reduces the size of the array to the specified size. If the array is already smaller than the specified size, no action is
         * taken. */
        public void Truncate(int newSize)
        {
            if (size <= newSize) return;
            for (int i = newSize; i < size; i++)
            {
                items[i] = default(T);
            }
            size = newSize;
        }

        /** Returns a random item from the array, or null if the array is empty. */
        public T Random()
        {
            if (size == 0)
            {
                return default(T);
            }
            return items[MathUtils.Random(0, size - 1)];
        }

        /** Returns the items as an array. Note the array is typed, so the {@link #Array(Class)} constructor must have been used.
         * Otherwise use {@link #toArray(Class)} to specify the array type. */
        public T[] ToArray()
        {
            return (T[])ToArray(items.GetType().BaseType);
        }

        public T[] ToArray(Type type)
        {
            T[] result = (T[])ArrayReflection.NewInstance(type, size);
            Array.Copy(items, 0, result, 0, size);
            return result;
        }

        public int HashCode()
        {
            if (!ordered) return base.GetHashCode();
            T[] items = this.items;
            int h = 1;
            for (int i = 0, n = size; i < n; i++)
            {
                h *= 31;
                Object item = items[i];
                if (item != null)
                {
                    h += item.GetHashCode();
                }
            }
            return h;
        }

        public bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (!ordered)
            {
                return false;
            }
            
            Utils.Array<T> array = (Utils.Array<T>)obj;
            if (!array.ordered)
            {
                return false;
            }
            int n = size;
            if (n != array.size)
            {
                return false;
            }
            T[] items1 = this.items;
            T[] items2 = array.items;
            for (int i = 0; i < n; i++)
            {
                object o1 = items1[i];
                object o2 = items2[i];
                if (!(o1 == null ? o2 == null : o1.Equals(o2))) return false;
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public String ToString()
        {
            if (size == 0) return "[]";
            T[] items = this.items;
            StringBuilder buffer = new StringBuilder(32);
            buffer.Append('[');
            buffer.Append(items[0]);
            for (int i = 1; i < size; i++)
            {
                buffer.Append(", ");
                buffer.Append(items[i]);
            }
            buffer.Append(']');
            return buffer.ToString();
        }

        public String ToString(String separator)
        {
            if (size == 0) return "";
            T[] items = this.items;
            StringBuilder buffer = new StringBuilder(32);
            buffer.Append(items[0]);
            for (int i = 1; i < size; i++)
            {
                buffer.Append(separator);
                buffer.Append(items[i]);
            }
            return buffer.ToString();
        }

        /** @see #Array(Class) */
        public static Array<T> Of(Type arrayType)
        {
            return new Array<T>(arrayType);
        }

        /** @see #Array(bool, int, Class) */
        public static Array<T> Of(bool ordered, int capacity, Type arrayType)
        {
            return new Array<T>(ordered, capacity, arrayType);
        }

        /** @see #Array(Object[]) */
        public static Array<T> With(params T[] array)
        {
            return new Array<T>(array);
        }
    }
}