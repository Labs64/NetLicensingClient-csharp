﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents Licensee. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Licensee.html
    /// </summary>
    public class Licensee : BaseEntity
    {
        /// <summary>
        /// The number of the product licensed to this licensee. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Licensee.html
        /// </summary>
        public String productNumber { get; set; }

        /// <summary>
        /// Custom properties of the licensee. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Licensee.html
        /// </summary>
        public Dictionary<String, String> licenseeProperties { get; private set; }

        // default constructor
        public Licensee()
        {
            licenseeProperties = new Dictionary<String, String>();
        }

        // construct from REST response item
        internal Licensee(item source)
        {
            if (!Constants.Licensee.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.Licensee.TYPE_NAME));
            }
            licenseeProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                switch (p.name)
                {
                    case Constants.Product.PRODUCT_NUMBER:
                        productNumber = p.Value;
                        break;
                    default:
                        if (!base.setFromProperty(p)) // Not BaseEntity property?
                        {
                            // custom property
                            if (!licenseeProperties.ContainsKey(p.name))
                            {
                                licenseeProperties.Add(p.name, p.Value);
                            }
                        }
                        break;
                }
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.Licensee.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            sb.Append(", ");
            sb.Append(Constants.Product.PRODUCT_NUMBER);
            sb.Append("=");
            sb.Append(productNumber);
            foreach (KeyValuePair<String, String> prop in licenseeProperties)
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
            if (productNumber != null) dict[Constants.Product.PRODUCT_NUMBER] = productNumber;
            foreach (KeyValuePair<String, String> prop in licenseeProperties)
            {
                dict[prop.Key] = prop.Value;
            }
            return dict;
        }
    }
}
