using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;
using System.Data;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the License Service. See NetLicensingAPI for details:
    /// https://www.labs64.de/confluence/display/NLICPUB/License+Services
    /// </summary>
    public class LicenseService
    {
        /// <summary>
        /// Creates new license object with given properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/License+Services#LicenseServices-Createlicense
        /// </summary>
        public static License create(Context context, String licenseeNumber, String licenseTemplateNumber, String transactionNumber, License newLicense)
        {
            newLicense.licenseeNumber = licenseeNumber;
            newLicense.licenseTemplateNumber = licenseTemplateNumber;
            string transactionOldValue;
            if (newLicense.licenseProperties.TryGetValue(Constants.Transaction.TRANSACTION_NUMBER, out transactionOldValue))
            {
                newLicense.licenseProperties.Remove(Constants.Transaction.TRANSACTION_NUMBER);
            }
            newLicense.licenseProperties.Add(Constants.Transaction.TRANSACTION_NUMBER, transactionNumber);
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.License.ENDPOINT_PATH, newLicense.ToDictionary());
            return new License(output.items.item[0]);
        }

        /// <summary>
        /// Gets license by its number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/License+Services#LicenseServices-Getlicense
        /// </summary>
        public static License get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.License.ENDPOINT_PATH + "/" + number, null);
            return new License(output.items.item[0]);
        }

        /// <summary>
        /// Returns all licenses of a vendor. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/License+Services#LicenseServices-Licenseslist
        /// </summary>
        public static List<License> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter))
            {
                parameters.Add(Constants.FILTER, filter);
            } 

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.License.ENDPOINT_PATH, parameters);

            List<License> licenses = new List<License>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    licenses.Add (new License (i));
                }
            }
            return licenses;
        }

        /// <summary>
        /// Updates license properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/License+Services#LicenseServices-Updatelicense
        /// </summary>
        public static License update(Context context, String number, String transactionNumber, License updateLicense)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.License.ENDPOINT_PATH + "/" + number, updateLicense.ToDictionary());
            return new License(output.items.item[0]);
        }

        /// <summary>
        /// Deletes license. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/License+Services#LicenseServices-Deletelicense
        /// </summary>
        public static void delete(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.License.ENDPOINT_PATH + "/" + number, null);
        }

    }
}
