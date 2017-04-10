using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Token Service. See NetLicensingAPI for details:
    /// https://www.labs64.de/confluence/display/NLICPUB/Token+Services
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Genarates token by its number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Token+Services#TokenServices-Createtoken
        /// </summary>
        public static Token create(Context context, Token newToken)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (newToken.tokenType == null)
            {
                newToken.tokenType = Constants.Token.TYPE_DEFAULT;
            }
            parameters.Add(Constants.Token.TOKEN_TYPE, newToken.tokenType);
            if (newToken.tokenType.Equals(Constants.Token.TYPE_SHOP) && newToken.tokenProperties.ContainsKey(Constants.Licensee.LICENSEE_NUMBER))
            {
                String licenseeNumber = newToken.tokenProperties[Constants.Licensee.LICENSEE_NUMBER];
                if (!String.IsNullOrEmpty(licenseeNumber))
                {
                    parameters.Add(Constants.Licensee.LICENSEE_NUMBER, licenseeNumber);
                }
            }

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Token.ENDPOINT_PATH, parameters);
            return new Token(output.items.item[0]);
        }

        /// <summary>
        /// Delete token by number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Token+Services#TokenServices-Deletetoken
        /// </summary>
        public static void delete(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Token.ENDPOINT_PATH + "/" + number, null);
        }

        /// <summary>
        /// Gets token by its number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Token+Services#TokenServices-Gettoken
        /// </summary>
        public static Token get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Token.ENDPOINT_PATH + "/" + number, null);
            return new Token(output.items.item[0]);
        }

        /// <summary>
        /// Returns all tokens of a vendor. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Token+Services#TokenServices-Tokenslist
        /// </summary>
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
