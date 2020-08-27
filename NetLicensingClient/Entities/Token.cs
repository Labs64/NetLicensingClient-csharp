﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents Token. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Token.html
    /// </summary>
    public class Token : BaseEntity
    {
        /// <summary>
        /// Type of the token. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Token.html
        /// </summary>
        public String tokenType { get; set; }

        /// <summary>
        /// Custom properties of token. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Token.html
        /// </summary>
        public Dictionary<String, String> tokenProperties { get; private set; }

        // default constructor
        public Token()
        {
            tokenProperties = new Dictionary<String, String>();
        }

        // construct from REST response item
        internal Token(item source)
        {
            if (!Constants.Token.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.Token.TYPE_NAME));
            }
            tokenProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                if (!base.setFromProperty(p)) // Not BaseEntity property?
                {
                    // custom property
                    if (!tokenProperties.ContainsKey(p.name))
                    {
                        tokenProperties.Add(p.name, p.Value);
                    }
                }
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.Token.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            foreach (KeyValuePair<String, String> prop in tokenProperties)
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
            dict.Add(Constants.Token.TOKEN_TYPE, (tokenType == null) ? Constants.Token.TYPE_DEFAULT : tokenType);
            foreach (KeyValuePair<String, String> prop in tokenProperties)
            {
                dict[prop.Key] = prop.Value;
            }
            return dict;
        }

    }

}
