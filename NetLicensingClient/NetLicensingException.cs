using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient
{
    public class NetLicensingException : Exception
    {

        public NetLicensingException(string message) : base(message) { }

        public NetLicensingException(string message, Exception innerException) :
            base(message, innerException) { }

    }
}
