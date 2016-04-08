using System.Collections;
using System.Collections.Generic;

namespace libcdx.Utils
{
    public class PredicateIterator<T> : IEnumerator<T>
    {
        public IEnumerator<T> iterator;
        public IPredicate<T> predicate;
        public bool end = false;
        public bool peeked = false;
        public T nextT = default(T);

        public PredicateIterator(IEnumerable<T> iterable, IPredicate<T> predicate) : this(iterable.GetEnumerator(), predicate)
        {
        }

        public PredicateIterator(IEnumerator<T> iterator, IPredicate<T> predicate)
        {
            Set(iterator, predicate);
        }

        public void Set(IEnumerable<T> iterable, IPredicate<T> predicate)
        {
            Set(iterable.GetEnumerator(), predicate);
        }

        public void Set(IEnumerator<T> iterator, IPredicate<T> predicate)
        {
            this.iterator = iterator;
            this.predicate = predicate;
            end = peeked = false;
            nextT = default(T);
        }

        public bool HasNext()
        {
            if (end)
            {
                return false;
            }
            if (nextT != null)
            {
                return true;
            }
            peeked = true;
            while (iterator.MoveNext())
            {
                T n = iterator.Current;
                if (predicate.Evaluate(n))
                {
                    nextT = n;
                    return true;
                }
            }
            end = true;
            return false;
        }

        public T Next()
        {
            if (nextT == null && !HasNext())
            {
                return default(T);
            }
            T result = nextT;
            nextT = default(T);
            peeked = false;
            return result;
        }

        public void Remove()
        {
            if (peeked)
            {
                throw new CdxRuntimeException("Cannot remove between a call to hasNext() and next().");
            }
            iterator.Dispose();
        }

        public void Dispose()
        {
            Remove();
        }

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public T Current { get; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}