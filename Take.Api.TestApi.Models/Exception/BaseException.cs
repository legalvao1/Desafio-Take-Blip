using System;
using System.Runtime.Serialization;

namespace Take.Api.BancoPanCartoes.Models.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }
        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
