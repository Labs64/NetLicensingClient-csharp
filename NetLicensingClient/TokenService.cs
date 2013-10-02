using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Token Service. See NetLicensingAPI JavaDoc for details:
    /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/TokenService.html
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Genarates token by its number. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/TokenService.html
        /// </summary>
        public static Token generate(Context context, String tokenType, String licenseeNumber)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (tokenType == null)
            {
                tokenType = Constants.Token.TYPE_DEFAULT;
            }
            parameters.Add("tokenType", tokenType);
            if (licenseeNumber != null && licenseeNumber.Length > 0 && tokenType.Equals(Constants.Token.TYPE_SHOP))
            {
                parameters.Add("licenseeNumber", licenseeNumber);
            }

            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Token.ENDPOINT_PATH, parameters);
            return new Token(output.items[0]);
        }
    }
}
