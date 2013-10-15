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
    /// C# representation of the Licensee Service. See NetLicensingAPI JavaDoc for details:
    /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
    /// </summary>
    public class LicenseeService
    {
        /// <summary>
        /// Creates new licensee object with given properties. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
        /// </summary>
        public static Licensee create(Context context, String productNumber, Licensee newLicensee)
        {
            newLicensee.productNumber = productNumber;
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH, newLicensee.ToDictionary());
            return new Licensee(output.items[0]);
        }

        /// <summary>
        /// Gets licensee by its number. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
        /// </summary>
        public static Licensee get(Context context, String number)
        {
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Licensee.ENDPOINT_PATH + "/" + number, null);
            return new Licensee(output.items[0]);
        }

        /// <summary>
        /// Returns all licensees of a vendor. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
        /// </summary>
        public static List<Licensee> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (filter != null && filter.Length > 0) 
            {
                parameters.Add("filter", filter);
            } 

            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Licensee.ENDPOINT_PATH, parameters);

            List<Licensee> licensees = new List<Licensee>();
            foreach (item i in output.items)
            {
                licensees.Add(new Licensee(i));
            }
            return licensees;
        }

        /// <summary>
        /// Updates licensee properties. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
        /// </summary>
        public static Licensee update(Context context, String number, Licensee updateLicensee)
        {
            updateLicensee.number = number;
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number, updateLicensee.ToDictionary());
            return new Licensee(output.items[0]);
        }

        /// <summary>
        /// Deletes licensee. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
        /// </summary>
        public static void delete(Context context, String number, Boolean forceCascade)
        {
            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Licensee.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

        /// <summary>
        /// Validates active licenses of the licensee. See NetLicensingAPI JavaDoc for details:
        /// http://netlicensing.labs64.com/javadoc/index.html?com/labs64/NetLicensing/core/service/LicenseeService.html
        /// </summary>
        public static ValidationResult validate(Context context, String number, String productNumber)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (productNumber != null || productNumber.Length > 0) 
            {
                parameters.Add("productNumber", productNumber);
            } 

            NetLicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Licensee.ENDPOINT_PATH + "/" + number + "/validate", parameters);
            return new ValidationResult(output);
        }

    }
}
