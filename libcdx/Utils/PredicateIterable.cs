using System.Collections;
using System.Collections.Generic;

namespace libcdx.Utils
{
    public class PredicateIterable<T> : IEnumerable<T>
    {
        public IEnumerable<T> iterable;
        public IPredicate<T> predicate;
        public PredicateIterator<T> iterator = null;

        public PredicateIterable(IEnumerable<T> iterable, IPredicate<T> predicate)
        {
            Set(iterable, predicate);
        }

        public void Set(IEnumerable<T> iterable, IPredicate<T> predicate)
        {
            this.iterable = iterable;
            this.predicate = predicate;
        }

        /** Returns an iterator. Note that the same iterator instance is returned each time this method is called. Use the
		 * {@link Predicate.PredicateIterator} constructor for nested or multithreaded iteration. */
        public IEnumerator<T> Iterator()
        {
            if (iterator == null)
            {
                iterator = new PredicateIterator<T>(iterable.GetEnumerator(), predicate);
            }
            else
            {
                iterator.Set(iterable.GetEnumerator(), predicate);
            }
            return iterator;
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