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
    /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/TokenService.html
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Genarates token by its number. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/TokenService.html
        /// </summary>
        public static Token create(Context context, Token newToken)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Token.ENDPOINT_PATH, newToken.ToDictionary());
            return new Token(output.items.item[0]);
        }

        /// <summary>
        /// Delete token by number. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/TokenService.html
        /// </summary>
        public static void delete(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Token.ENDPOINT_PATH + "/" + number, null);
        }

        /// <summary>
        /// Gets token by its number. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/TokenService.html
        /// </summary>
        public static Token get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Token.ENDPOINT_PATH + "/" + number, null);
            return new Token(output.items.item[0]);
        }

        public static List<Token> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter))
            {
                parameters.Add(Constants.FILTER, filter);
            }

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Token.ENDPOINT_PATH, parameters);

            List<Token> tokens = new List<Token>();
            if (output.items.item != null)
            {
                foreach (item i in output.items.item)
                {
                    tokens.Add(new Token(i));
                }
            }
            return tokens;
        }

    }

}
