using System;
using System.Collections.Generic;
using System.Text;

namespace MFR.Persistence.Utils
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(){}
        public EntityNotFoundException(string message) : base(message){}
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException){}
    }
}
