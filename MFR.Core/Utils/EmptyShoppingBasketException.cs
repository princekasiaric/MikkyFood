using System;

namespace MFR.Core.Utils
{
    public class EmptyShoppingBasketException : Exception
    {
        public EmptyShoppingBasketException() { }
        public EmptyShoppingBasketException(string message) : base(message) { }
        public EmptyShoppingBasketException(string message, Exception innerException) : base(message, innerException) { }
    }
}
