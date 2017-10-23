using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Exceptions
{
    class RestException : System.Exception
    {
        public RestException() : base() { }
        public RestException(string message) : base(message) { }
        public RestException(string message, System.Exception inner) : base(message, inner) { }
        protected RestException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
