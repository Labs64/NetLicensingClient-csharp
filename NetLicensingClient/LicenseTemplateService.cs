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
    /// C# representation of the LicenseTemplate Service. See NetLicensingAPI JavaDoc for details:
    /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseTemplateService.html
    /// </summary>
    public class LicenseTemplateService
    {
        /// <summary>
        /// Creates new license template object with given properties. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static LicenseTemplate create(Context context, String productModuleNumber, LicenseTemplate newLicenseTemplate)
        {
            newLicenseTemplate.productModuleNumber = productModuleNumber;
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.LicenseTemplate.ENDPOINT_PATH, newLicenseTemplate.ToDictionary());
            return new LicenseTemplate(output.items[0]);
        }

        /// <summary>
        /// Gets license template by its number. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static LicenseTemplate get(Context context, String number)
        {
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.LicenseTemplate.ENDPOINT_PATH + "/" + number, null);
            return new LicenseTemplate(output.items[0]);
        }

        /// <summary>
        /// Returns all license templates of a vendor. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static List<LicenseTemplate> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (filter != null && filter.Length > 0)
            {
                parameters.Add("filter", filter);
            }

            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.LicenseTemplate.ENDPOINT_PATH, parameters);

            List<LicenseTemplate> licenseTemplates = new List<LicenseTemplate>();
            foreach (item i in output.items)
            {
                licenseTemplates.Add(new LicenseTemplate(i));
            }
            return licenseTemplates;
        }
        /// <summary>
        /// Updates license template properties. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static LicenseTemplate update(Context context, String number, LicenseTemplate updateLicenseTemplate)
        {
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.LicenseTemplate.ENDPOINT_PATH + "/" + number, updateLicenseTemplate.ToDictionary());
            return new LicenseTemplate(output.items[0]);
        }

        /// <summary>
        /// Deletes license template. See NetLicensingAPI JavaDoc for details:
        /// http://NetLicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseTemplateService.html
        /// </summary>
        public static void delete(Context context, String number, Boolean forceCascade)
        {
            String strCascade = Convert.ToString(forceCascade).ToLower();
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.LicenseTemplate.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

    }
}