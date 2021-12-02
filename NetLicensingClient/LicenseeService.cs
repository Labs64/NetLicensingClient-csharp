using System;
using System.Collections.Generic;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Licensee Service. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
    /// </summary>
    public class LicenseeService
    {
        /// <summary>
        /// Creates new licensee object with given properties. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
        /// </summary>
        public static Licensee create(Context context, String productNumber, Licensee newLicensee)
        {
            newLicensee.productNumber = productNumber;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH, newLicensee.ToDictionary());
            return new Licensee(output.items.item[0]);
        }

        /// <summary>
        /// Gets licensee by its number. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
        /// </summary>
        public static Licensee get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Licensee.ENDPOINT_PATH + "/" + number, null);
            return new Licensee(output.items.item[0]);
        }

        /// <summary>
        /// Returns all licensees of a vendor. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
        /// </summary>
        public static List<Licensee> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter)) 
            {
                parameters.Add(Constants.FILTER, filter);
            } 

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Licensee.ENDPOINT_PATH, parameters);

            List<Licensee> licensees = new List<Licensee>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    licensees.Add (new Licensee (i));
                }
            }
            return licensees;
        }

        /// <summary>
        /// Updates licensee properties. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
        /// </summary>
        public static Licensee update(Context context, String number, Licensee updateLicensee)
        {
            updateLicensee.number = number;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number, updateLicensee.ToDictionary());
            return new Licensee(output.items.item[0]);
        }

        /// <summary>
        /// Deletes licensee. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
        /// </summary>
        public static void delete(Context context, String number, Boolean forceCascade)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Licensee.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

        /// <summary>
        /// Validates active licenses of the licensee. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/LicenseeService.html
        /// </summary>
        [System.Obsolete("validate(Context, String, String, String, ValidationParameters) is obsolete, use validate(Context, String, ValidationParameters) instead")]
        public static ValidationResult validate(Context context, String number, String productNumber, String licenseeName, ValidationParameters validationParameters)
        {
            if (!String.IsNullOrEmpty(productNumber)) 
            {
                validationParameters.setProductNumber(productNumber);
            }
            if (!String.IsNullOrEmpty(licenseeName)) 
            {
                validationParameters.setLicenseeName(licenseeName);
            }

            return validate(context, number, validationParameters);
        }

        /// <summary>
        /// Validates active licenses of the licensee.
        /// In the case of multiple product modules validation, required parameters indexes will be added automatically.
        /// See NetLicensingAPI for details: https://netlicensing.io/wiki/licensee-services#validate-licensee
        /// </summary>
        public static ValidationResult validate(Context context, String number, ValidationParameters validationParameters, int timeoutInMilliseconds = 100000)
        {
        	Dictionary<String, String> parameters = new Dictionary<String, String> ();
            if (!String.IsNullOrEmpty(validationParameters.getProductNumber())) 
            {
                parameters.Add(Constants.Product.PRODUCT_NUMBER, validationParameters.getProductNumber());
            }
            if (!String.IsNullOrEmpty(validationParameters.getLicenseeName())) 
            {
                parameters.Add(Constants.Licensee.PROP_LICENSEE_NAME, validationParameters.getLicenseeName());
            }
#pragma warning disable 0618
            // This section is only left to verify backwards compatibility.
            // Don't use LicenseeSecret, use Node-Locked licensing model instead.
            if (!String.IsNullOrEmpty(validationParameters.getLicenseeSecret())) 
            {
                parameters.Add(Constants.Licensee.PROP_LICENSEE_SECRET, validationParameters.getLicenseeSecret());
            }
#pragma warning restore 0618

            int pmIndex = 0;
        	foreach (KeyValuePair<String, Dictionary<String, String>> productModuleValidationParams in validationParameters.getParameters ()) {
        		parameters.Add (Constants.ProductModule.PRODUCT_MODULE_NUMBER + pmIndex, productModuleValidationParams.Key);
        		foreach (KeyValuePair<String, String> param in productModuleValidationParams.Value) {
        			parameters.Add (param.Key + pmIndex, param.Value);
        		}
        		pmIndex++;
        	}

        	netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number + "/" + Constants.Licensee.ENDPOINT_PATH_VALIDATE, parameters, timeoutInMilliseconds);
        	return new ValidationResult (output);
        }

        /// <summary>
        /// Transfer licenses between licensees.
        /// https://netlicensing.io/wiki/licensee-services#transfer-licenses
        /// </summary>
        public static void transfer (Context context, String number, String sourceLicenseeNumber)
        {
        	Dictionary<String, String> parameters = new Dictionary<String, String> ();
        	if (!String.IsNullOrEmpty (sourceLicenseeNumber)) {
        		parameters.Add ("sourceLicenseeNumber", sourceLicenseeNumber);
        	}
        	NetLicensingAPI.request (context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number + "/" + Constants.Licensee.ENDPOINT_PATH_TRANSFER, parameters);
        }

    }
}
