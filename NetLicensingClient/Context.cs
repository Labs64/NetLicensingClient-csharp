using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{

    /// <summary>
    /// Enumerates possible security modes for accessing the NetLicensing API
    /// See https://netlicensing.io/wiki/security for details.
    /// </summary>
    public enum SecurityMode
    {
        BASIC_AUTHENTICATION,
        APIKEY_IDENTIFICATION,
        ANONYMOUS_IDENTIFICATION
    };

    /// <summary>
    /// Holds the common context for all calls to the NetLicensingAPI RESTful, in particular server URL and login credentials.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Server URL base of the NetLicensing RESTful API. Normally should be "https://go.netlicensing.io/core/v2/rest".
        /// </summary>
        public String baseUrl { get; set; } = "https://go.netlicensing.io/core/v2/rest";

        /// <summary>
        /// Login name of the user sending the requests when securityMode = BASIC_AUTHENTICATION.
        /// </summary>
        public String username { get; set; }

        /// <summary>
        /// Password of the user sending the requests when securityMode = BASIC_AUTHENTICATION.
        /// </summary>
        public String password { get; set; }

        /// <summary>
        /// API Key used to identify the request sender when securityMode = APIKEY_IDENTIFICATION.
        /// </summary>
        public String apiKey { get; set; }

        /// <summary>
        /// Determines the security mode used for accessing the NetLicensing API.
        /// See https://netlicensing.io/wiki/security for details.
        /// </summary>
        public SecurityMode securityMode { get; set; }

        /// <summary>
        /// External number of the vendor.
        /// </summary>
        public String vendorNumber { get; set; }

        /// <summary>
        /// Public key to verify XML signature.
        /// </summary>
        public String publicKey { get; set; }

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
