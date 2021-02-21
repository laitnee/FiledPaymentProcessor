using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Core.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }
        public NotFoundException(string message) : base(message: message)
        {
        }
        public NotFoundException(string message, Exception inner) : base(message: message, innerException: inner)
        {
        }
    }
}
