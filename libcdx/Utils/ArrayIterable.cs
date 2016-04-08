using System.Collections;
using System.Collections.Generic;

namespace libcdx.Utils
{
    public class ArrayIterable<T> : IEnumerable<T>
    {
        private readonly Array<T> array;
        private readonly bool  allowRemove;
		private ArrayIterator<T> iterator1, iterator2;

        // java.io.StringWriter lastAcquire = new java.io.StringWriter();

        public ArrayIterable(Array<T> array) : this(array, true)
        {
        }

        public ArrayIterable(Array<T> array, bool allowRemove)
        {
            this.array = array;
            this.allowRemove = allowRemove;
        }

        public IEnumerator<T> Iterator()
        {
            // lastAcquire.getBuffer().setLength(0);
            // new Throwable().printStackTrace(new java.io.PrintWriter(lastAcquire));
            if (iterator1 == null)
            {
                iterator1 = new ArrayIterator<T>(array, allowRemove);
                iterator2 = new ArrayIterator<T>(array, allowRemove);
                // iterator1.iterable = this;
                // iterator2.iterable = this;
            }
            if (!iterator1.Valid)
            {
                iterator1.Index = 0;
                iterator1.Valid = true;
                iterator2.Valid = false;
                return iterator1;
            }
            iterator2.Index = 0;
            iterator2.Valid = true;
            iterator1.Valid = false;
            return iterator2;
        }
        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}