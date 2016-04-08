using System;
using System.Text;

namespace libcdx.Utils
{
    public class IntArray
    {
        public int[] items;
        public int size;
        public bool ordered;

        /** Creates an ordered array with a capacity of 16. */
        public IntArray() : this(true, 16)
        {
        }

        /** Creates an ordered array with the specified capacity. */
        public IntArray(int capacity) : this(true, capacity)
        {
        }

        /** @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
         *           memory copy.
         * @param capacity Any elements added beyond this will cause the backing array to be grown. */
        public IntArray(bool ordered, int capacity)
        {
            this.ordered = ordered;
            items = new int[capacity];
        }

        /** Creates a new array containing the elements in the specific array. The new array will be ordered if the specific array is
         * ordered. The capacity is set to the number of elements, so any subsequent elements added will cause the backing array to be
         * grown. */
        public IntArray(IntArray array)
        {
            this.ordered = array.ordered;
            size = array.size;
            items = new int[size];
            Array.Copy(array.items, 0, items, 0, size);
        }

        /** Creates a new ordered array containing the elements in the specified array. The capacity is set to the number of elements,
         * so any subsequent elements added will cause the backing array to be grown. */
        public IntArray(int[] array) : this(true, array, 0, array.Length)
        {
        }

        /** Creates a new array containing the elements in the specified array. The capacity is set to the number of elements, so any
         * subsequent elements added will cause the backing array to be grown.
         * @param ordered If false, methods that remove elements may change the order of other elements in the array, which avoids a
         *           memory copy. */
        public IntArray(bool ordered, int[] array, int startIndex, int count) : this(ordered, count)
        {
            size = count;
            Array.Copy(array, startIndex, items, 0, count);
        }

        public void add(int value)
        {
            int[] items = this.items;
            if (size == items.Length) items = Resize(System.Math.Max(8, (int)(size * 1.75f)));
            items[size++] = value;
        }

        public void AddAll(IntArray array)
        {
            AddAll(array, 0, array.size);
        }

        public void AddAll(IntArray array, int offset, int length)
        {
            if (offset + length > array.size)
                throw new ArgumentOutOfRangeException("offset + length must be <= size: " + offset + " + " + length + " <= " + array.size);
            AddAll(array.items, offset, length);
        }

        public void AddAll(params int[] array)
        {
            AddAll(array, 0, array.Length);
        }

        public void AddAll(int[] array, int offset, int length)
        {
            int[] items = this.items;
            int sizeNeeded = size + length;
            if (sizeNeeded > items.Length) items = Resize(System.Math.Max(8, (int)(sizeNeeded * 1.75f)));
            Array.Copy(array, offset, items, size, length);
            size += length;
        }

        public int Get(int index)
        {
            if (index >= size) throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            return items[index];
        }

        public void Set(int index, int value)
        {
            if (index >= size) throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            items[index] = value;
        }

        public void Incr(int index, int value)
        {
            if (index >= size) throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            items[index] += value;
        }

        public void Mul(int index, int value)
        {
            if (index >= size) throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            items[index] *= value;
        }

        public void Insert(int index, int value)
        {
            if (index > size) throw new ArgumentOutOfRangeException("index can't be > size: " + index + " > " + size);
            int[] items = this.items;
            if (size == items.Length) items = Resize(System.Math.Max(8, (int)(size * 1.75f)));
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
            if (first >= size) throw new ArgumentOutOfRangeException("first can't be >= size: " + first + " >= " + size);
            if (second >= size) throw new ArgumentOutOfRangeException("second can't be >= size: " + second + " >= " + size);
            int[] items = this.items;
            int firstValue = items[first];
            items[first] = items[second];
            items[second] = firstValue;
        }

        public bool Contains(int value)
        {
            int i = size - 1;
            int[] items = this.items;
            while (i >= 0)
                if (items[i--] == value) return true;
            return false;
        }

        public int IndexOf(int value)
        {
            int[] items = this.items;
            for (int i = 0, n = size; i < n; i++)
                if (items[i] == value) return i;
            return -1;
        }

        public int LastIndexOf(int value)
        {
            int[] items = this.items;
            for (int i = size - 1; i >= 0; i--)
                if (items[i] == value) return i;
            return -1;
        }

        public bool RemoveValue(int value)
        {
            int[] items = this.items;
            for (int i = 0, n = size; i < n; i++)
            {
                if (items[i] == value)
                {
                    RemoveIndex(i);
                    return true;
                }
            }
            return false;
        }

        /** Removes and returns the item at the specified index. */
        public int RemoveIndex(int index)
        {
            if (index >= size) throw new ArgumentOutOfRangeException("index can't be >= size: " + index + " >= " + size);
            int[] items = this.items;
            int value = items[index];
            size--;
            if (ordered)
            {
                Array.Copy(items, index + 1, items, index, size - index);
            }
            else
            {
                items[index] = items[size];
            }
            return value;
        }

        /** Removes the items between the specified indices, inclusive. */
        public void removeRange(int start, int end)
        {
            if (end >= size) throw new ArgumentOutOfRangeException("end can't be >= size: " + end + " >= " + size);
            if (start > end) throw new ArgumentOutOfRangeException("start can't be > end: " + start + " > " + end);
            int[] items = this.items;
            int count = end - start + 1;
            if (ordered)
            {
                Array.Copy(items, start + count, items, start, size - (start + count));
            }
            else
            {
                int lastIndex = this.size - 1;
                for (int i = 0; i < count; i++)
                    items[start + i] = items[lastIndex - i];
            }
            size -= count;
        }

        /** Removes from this array all of elements contained in the specified array.
         * @return true if this array was modified. */
        public bool removeAll(IntArray array)
        {
            int size = this.size;
            int startSize = size;
            int[] items = this.items;
            for (int i = 0, n = array.size; i < n; i++)
            {
                int item = array.Get(i);
                for (int ii = 0; ii < size; ii++)
                {
                    if (item == items[ii])
                    {
                        RemoveIndex(ii);
                        size--;
                        break;
                    }
                }
            }
            return size != startSize;
        }

        /** Removes and returns the last item. */
        public int Pop()
        {
            return items[--size];
        }

        /** Returns the last item. */
        public int Peek()
        {
            return items[size - 1];
        }

        /** Returns the first item. */
        public int First()
        {
            if (size == 0) throw new ArgumentOutOfRangeException("Array is empty.");
            return items[0];
        }

        public void Clear()
        {
            size = 0;
        }

        /** Reduces the size of the backing array to the size of the actual items. This is useful to release memory when many items have
         * been removed, or if it is known that more items will not be added.
         * @return {@link #items} */
        public int[] Shrink()
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
        public int[] EnsureCapacity(int additionalCapacity)
        {
            int sizeNeeded = size + additionalCapacity;
            if (sizeNeeded > items.Length)
            {
                Resize(System.Math.Max(8, sizeNeeded));
            }
            return items;
        }

        protected int[] Resize(int newSize)
        {
            int[] newItems = new int[newSize];
            int[] items = this.items;
            Array.Copy(items, 0, newItems, 0, System.Math.Min(size, newItems.Length));
            this.items = newItems;
            return newItems;
        }

        public void Sort()
        {
            Array.Sort(items, 0, size);
        }

        public void Reverse()
        {
            int[] items = this.items;
            for (int i = 0, lastIndex = size - 1, n = size / 2; i < n; i++)
            {
                int ii = lastIndex - i;
                int temp = items[i];
                items[i] = items[ii];
                items[ii] = temp;
            }
        }

        public void Shuffle()
        {
            int[] items = this.items;
            Random random = new Random();
            for (int i = size - 1; i >= 0; i--)
            {
                int ii = random.Next(i);
                int temp = items[i];
                items[i] = items[ii];
                items[ii] = temp;
            }
        }

        /** Reduces the size of the array to the specified size. If the array is already smaller than the specified size, no action is
         * taken. */
        public void Truncate(int newSize)
        {
            if (size > newSize) size = newSize;
        }

        /** Returns a random item from the array, or zero if the array is empty. */
        public int Random()
        {
            if (size == 0) return 0;
            Random random = new Random();
            return items[random.Next(0, size - 1)];
        }

        public int[] ToArray()
        {
            int[] array = new int[size];
            Array.Copy(items, 0, array, 0, size);
            return array;
        }

        public int HashCode()
        {
            if (!ordered) return base.GetHashCode();
            int[] items = this.items;
            int h = 1;
            for (int i = 0, n = size; i < n; i++)
                h = h * 31 + items[i];
            return h;
        }

        public new bool Equals(object obj)
        {
            if (obj == this) return true;
            if (!ordered) return false;
            if (!(obj.GetType() == typeof (IntArray))) return false;
            IntArray array = (IntArray)obj;
            if (!array.ordered) return false;
            int n = size;
            if (n != array.size) return false;
            int[] items1 = this.items;
            int[] items2 = array.items;
            for (int i = 0; i < n; i++)
                if (items[i] != array.items[i]) return false;
            return true;
        }

        public new string ToString()
        {
            if (size == 0) return "[]";
            int[] items = this.items;
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

        public string ToString(string separator)
        {
            if (size == 0) return "";
            int[] items = this.items;
            StringBuilder buffer = new StringBuilder(32);
            buffer.Append(items[0]);
            for (int i = 1; i < size; i++)
            {
                buffer.Append(separator);
                buffer.Append(items[i]);
            }
            return buffer.ToString();
        }

        /** @see #IntArray(int[]) */
        static public IntArray with(params int[] array)
        {
            return new IntArray(array);
        }
    }
}