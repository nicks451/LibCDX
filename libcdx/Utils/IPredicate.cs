namespace libcdx.Utils
{
    public interface IPredicate<T>
    {
        /** @return true if the item matches the criteria and should be included in the iterator's items */
        bool Evaluate(T arg0);
    }
}