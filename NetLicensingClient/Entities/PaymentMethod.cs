using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents Payment Method. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/PaymentMethod.html
    /// </summary>
    public class PaymentMethod : BaseEntity
    {
        /// <summary>
        /// Custom properties of the payment method. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/PaymentMethod.html
        /// </summary>
        public Dictionary<String, String> paymentMethodProperties { get; private set; }

        // default constructor
        public PaymentMethod()
        {
            paymentMethodProperties = new Dictionary<String, String>();
        }

        // construct from REST response item
        internal PaymentMethod(item source)
        {
            if (!Constants.PaymentMethod.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.PaymentMethod.TYPE_NAME));
            }
            paymentMethodProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                if (!base.setFromProperty(p)) // Not BaseEntity property?
                {
                    // custom property
                    if (!paymentMethodProperties.ContainsKey(p.name))
                    {
                        paymentMethodProperties.Add(p.name, p.Value);
                    }
                }
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.PaymentMethod.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            foreach (KeyValuePair<String, String> prop in paymentMethodProperties)
            {
                sb.Append(", ");
                sb.Append(prop.Key);
                sb.Append("=");
                sb.Append(prop.Value);
            }
            sb.Append("]");
            return sb.ToString();
        }

        internal new Dictionary<String, String> ToDictionary()
        {
            Dictionary<String, String> dict = base.ToDictionary();
            foreach (KeyValuePair<String, String> prop in paymentMethodProperties)
            {
                dict[prop.Key] = prop.Value;
            }
            return dict;
        }


    }
}
