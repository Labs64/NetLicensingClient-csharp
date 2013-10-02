﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// Holds the common context for all calls to the NetLicensingAPI RESTful, in particular server URL and login credentials.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Server URL base of NetLicensingAPI RESTful. Normally should be "https://NetLicensing.labs64.com".
        /// </summary>
        public String baseUrl { get; set; }

        /// <summary>
        /// Login name of the user sending the requests.
        /// </summary>
        public String username { get; set; }

        /// <summary>
        /// Password of the user sending the requests.
        /// </summary>
        public String password { get; set; }

        /// <summary>
        /// External number of the vendor.
        /// </summary>
        public String vendorNumber { get; set; }

        /// <summary>
        /// Use this call to form the redirection URL for NetLicensingShop.
        /// </summary>
        /// <param name="licenseeNumber">External number of the licensee that is going to shop</param>
        /// <returns>URL that is to be used to redirect licensee to NetLicensingShop</returns>
        public String getShopURL(String licenseeNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(baseUrl);
            sb.Append(Constants.SHOP_PATH);
            sb.Append("?vendorNumber=");
            sb.Append(vendorNumber);
            sb.Append("&licenseeNumber=");
            sb.Append(licenseeNumber);
            return sb.ToString();
        }
    }
}
