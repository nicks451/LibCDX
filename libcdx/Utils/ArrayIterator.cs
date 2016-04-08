using System;
using System.Collections;
using System.Collections.Generic;

namespace libcdx.Utils
{
    public class ArrayIterator<T> : IEnumerator<T>
    {
        private readonly Array<T> array;
        private readonly bool allowRemove;
        public int Index;
        public bool Valid = true;

        public ArrayIterator(Array<T> array) : this(array, true)
        {
        }

        public ArrayIterator(Array<T> array, bool allowRemove)
        {
            this.array = array;
            this.allowRemove = allowRemove;
        }

        public bool HasNext()
        {
            if (!Valid)
            {
                // System.out.println(iterable.lastAcquire);
                throw new CdxRuntimeException("#iterator() cannot be used nested.");
            }
            return Index < array.size;
        }

        public T Next()
        {
            if (Index >= array.size)
            {
                throw new ArgumentOutOfRangeException(Index.ToString());
            }
            if (!Valid)
            {
                // System.out.println(iterable.lastAcquire);
                throw new CdxRuntimeException("#iterator() cannot be used nested.");
            }
            return array.items[Index++];
        }

        public void Remove()
        {
            if (!allowRemove)
            {
                throw new CdxRuntimeException("Remove not allowed.");
            }
            Index--;
            array.RemoveIndex(Index);
        }

        public void Reset()
        {
            Index = 0;
        }

        public IEnumerator<T> Iterator()
        {
            return this;
        }

        public void Dispose()
        {
            Remove();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public T Current { get; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}