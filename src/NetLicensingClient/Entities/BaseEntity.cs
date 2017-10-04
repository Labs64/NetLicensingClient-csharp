using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Defines common entity fields. See NetLicensingAPI JavaDoc for details:
    /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/common/domain/entity/BaseDBEntity.html
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        // Properties
        public String number { get; set; }
        public Boolean? active { get; set; }

        // returns true if property was consumed.
        internal bool setFromProperty(property p)
        {
            switch (p.name)
            {
                case Constants.NUMBER:
                    number = p.Value;
                    return true;
                case Constants.ACTIVE:
                    active = Utilities.CheckedParseBoolean(p.Value, Constants.ACTIVE);
                    return true;
            }
            return false;
        }

        internal static void verifyTypeIsString(object o)
        {
            if (!(o is String))
            {
                throw new NetLicensingException(String.Format("Expected string type, got '{0}'", o.GetType()));
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.NUMBER);
            sb.Append("=");
            sb.Append(number);
            sb.Append(", ");
            sb.Append(Constants.ACTIVE);
            sb.Append("=");
            sb.Append(active);
            return sb.ToString();
        }

        internal Dictionary<String, String> ToDictionary()
        {
            Dictionary<String, String> dict = new Dictionary<String, String>();
            if (number != null) dict[Constants.NUMBER] = number;
            if (active.HasValue) dict[Constants.ACTIVE] = active.ToString();
            return dict;
        }
    }
}
