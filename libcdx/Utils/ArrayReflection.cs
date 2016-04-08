using System;
using System.Reflection;

namespace libcdx.Utils
{
    public class ArrayReflection
    {
        /** Creates a new array with the specified component type and length. */
        public static object NewInstance(Type c, int size)
        {
            return Array.CreateInstance(c, size);
        }

        /** Returns the length of the supplied array. */
        public static int GetLength(object array)
        {
            Array arr = (Array) array;
            return arr.Length;
        }

        /** Returns the value of the indexed component in the supplied array. */
        public static object Get(object array, int index)
        {
            Array arr = (Array) array;
            return arr.GetValue(index);
        }

        /** Sets the value of the indexed component in the supplied array to the supplied value. */
        public static void Set(object array, int index, object value)
        {
            Array arr = (Array) array;
            arr.SetValue(value, index);
        }
    }
}