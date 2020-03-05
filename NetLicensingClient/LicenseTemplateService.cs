using System;
using System.Collections.Generic;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the LicenseTemplate Service. See NetLicensingAPI JavaDoc for details:
    /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/LicenseTemplateService.html
    /// </summary>
    public class LicenseTemplateService
    {
        /// <summary>
        /// Creates new license template object with given properties. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static LicenseTemplate create(Context context, String productModuleNumber, LicenseTemplate newLicenseTemplate)
        {
            newLicenseTemplate.productModuleNumber = productModuleNumber;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.LicenseTemplate.ENDPOINT_PATH, newLicenseTemplate.ToDictionary());
            return new LicenseTemplate(output.items.item[0]);
        }

        /// <summary>
        /// Gets license template by its number. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static LicenseTemplate get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.LicenseTemplate.ENDPOINT_PATH + "/" + number, null);
            return new LicenseTemplate(output.items.item[0]);
        }

        /// <summary>
        /// Returns all license templates of a vendor. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static List<LicenseTemplate> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter))
            {
                parameters.Add(Constants.FILTER, filter);
            }

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.LicenseTemplate.ENDPOINT_PATH, parameters);

            List<LicenseTemplate> licenseTemplates = new List<LicenseTemplate>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    licenseTemplates.Add (new LicenseTemplate (i));
                }
            }
            return licenseTemplates;
        }
        /// <summary>
        /// Updates license template properties. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static LicenseTemplate update(Context context, String number, LicenseTemplate updateLicenseTemplate)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.LicenseTemplate.ENDPOINT_PATH + "/" + number, updateLicenseTemplate.ToDictionary());
            return new LicenseTemplate(output.items.item[0]);
        }

        /// <summary>
        /// Deletes license template. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/v2/com/labs64/netlicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static void delete(Context context, String number, Boolean forceCascade)
        {
            String strCascade = Convert.ToString(forceCascade).ToLower();
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.LicenseTemplate.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

    }
}