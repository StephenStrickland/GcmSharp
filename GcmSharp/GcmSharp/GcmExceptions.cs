using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GcmSharp
{
    class GcmManagerTimeoutException : Exception
    {
        public GcmManagerTimeoutException()
            : base() { }

        public GcmManagerTimeoutException(string message, GcmMessage gcmMessage)
            : base(message) { Message = gcmMessage; }

        public GcmManagerTimeoutException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public GcmManagerTimeoutException(string message, Exception innerException, GcmMessage gcmMessage)
            : base(message, innerException) { Message = gcmMessage; }

        public GcmManagerTimeoutException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected GcmManagerTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public GcmMessage Message { get; set; }
    }

    public class GcmException : Exception
    {
        public GcmException()
            : base() { }

        public GcmException(string message, GcmMessage gcmMessage)
            : base(message) { Message = gcmMessage; }

        public GcmException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public GcmException(string message, Exception innerException, GcmMessage gcmMessage)
            : base(message, innerException) { Message = gcmMessage; }

        public GcmException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected GcmException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public GcmMessage Message { get; set; }
    }
}
