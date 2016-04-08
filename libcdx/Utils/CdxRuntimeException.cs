using System;

namespace libcdx.Utils
{
    public class CdxRuntimeException : Exception
    {
        public CdxRuntimeException(string message) : base(message)
        {
            
        }

        public CdxRuntimeException(string message, Exception e) : base(message, e)
        {
            
        }
    }
}