﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents License. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/License.html
    /// </summary>
    public class License : BaseEntity
    {
        /// <summary>
        /// Licesnee number of the license owner. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/License.html
        /// </summary>
        public String licenseeNumber { get; set; }

        /// <summary>
        /// Licesne template number of this license. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/License.html
        /// </summary>
        public String licenseTemplateNumber { get; set; }

        /// <summary>
        /// Custom properties of the license. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/License.html
        /// </summary>
        public Dictionary<String, String> licenseProperties { get; private set; }

        // default constructor
        public License()
        {
            licenseProperties = new Dictionary<String, String>();
        }

        // construct from REST response item
        internal License(item source)
        {
            if (!Constants.License.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.License.TYPE_NAME));
            }
            licenseProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                switch (p.name)
                {
                    case Constants.Licensee.LICENSEE_NUMBER:
                        licenseeNumber = p.Value;
                        break;
                    case Constants.LicenseTemplate.LICENSE_TEMPLATE_NUMBER:
                        licenseTemplateNumber = p.Value;
                        break;
                    default:
                        if (!base.setFromProperty(p)) // Not BaseEntity property?
                        {
                            // custom property
                            if (!licenseProperties.ContainsKey(p.name))
                            {
                                licenseProperties.Add(p.name, p.Value);
                            }
                        }
                        break;
                }
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.License.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            sb.Append(", ");
            sb.Append(Constants.Licensee.LICENSEE_NUMBER);
            sb.Append("=");
            sb.Append(licenseeNumber);
            sb.Append(", ");
            sb.Append(Constants.LicenseTemplate.LICENSE_TEMPLATE_NUMBER);
            sb.Append("=");
            sb.Append(licenseTemplateNumber);
            foreach (KeyValuePair<String, String> prop in licenseProperties)
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
            if (licenseeNumber != null) dict[Constants.Licensee.LICENSEE_NUMBER] = licenseeNumber;
            if (licenseTemplateNumber != null) dict[Constants.LicenseTemplate.LICENSE_TEMPLATE_NUMBER] = licenseTemplateNumber;
            foreach (KeyValuePair<String, String> prop in licenseProperties)
            {
                dict[prop.Key] = prop.Value;
            }
            return dict;
        }
    }
}
