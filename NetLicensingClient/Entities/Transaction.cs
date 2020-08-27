using System;
using System.Collections.Generic;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents Transaction. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Transaction.html
    /// </summary>
    public class Transaction : BaseEntity
    {
        /// <summary>
        /// Custom properties of the transaction. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Transaction.html
        /// </summary>
        public Dictionary<String, String> transactionProperties { get; private set; }

        // default constructor
        public Transaction ()
        {
            transactionProperties = new Dictionary<String, String>();
        }

        // construct from REST response item
        internal Transaction(item source)
        {
            if (!Constants.Transaction.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.Transaction.TYPE_NAME));
            }
            transactionProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                if (!base.setFromProperty(p)) // Not BaseEntity property?
                {
                    // custom property
                    if (!transactionProperties.ContainsKey(p.name))
                    {
                        transactionProperties.Add(p.name, p.Value);
                    }
                }
            }
        }


        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.Transaction.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            foreach (KeyValuePair<String, String> prop in transactionProperties)
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
            foreach (KeyValuePair<String, String> prop in transactionProperties)
            {
                dict[prop.Key] = prop.Value;
            }
            return dict;
        }
    }
}

