﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;
using System.Data;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Licensee Service. See NetLicensingAPI for details:
    /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services
    /// </summary>
    public class LicenseeService
    {
        /// <summary>
        /// Creates new licensee object with given properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services#LicenseeServices-Createlicensee
        /// </summary>
        public static Licensee create(Context context, String productNumber, Licensee newLicensee)
        {
            newLicensee.productNumber = productNumber;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH, newLicensee.ToDictionary());
            return new Licensee(output.items.item[0]);
        }

        /// <summary>
        /// Gets licensee by its number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services#LicenseeServices-Getlicensee
        /// </summary>
        public static Licensee get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Licensee.ENDPOINT_PATH + "/" + number, null);
            return new Licensee(output.items.item[0]);
        }

        /// <summary>
        /// Returns all licensees of a vendor. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services#LicenseeServices-Licenseeslist
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
        /// Updates licensee properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services#LicenseeServices-Updatelicensee
        /// </summary>
        public static Licensee update(Context context, String number, Licensee updateLicensee)
        {
            updateLicensee.number = number;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number, updateLicensee.ToDictionary());
            return new Licensee(output.items.item[0]);
        }

        /// <summary>
        /// Deletes licensee. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services#LicenseeServices-Deletelicensee
        /// </summary>
        public static void delete(Context context, String number, Boolean forceCascade)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Licensee.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

        /// <summary>
        /// Validates active licenses of the licensee. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Licensee+Services#LicenseeServices-Validatelicensee
        /// </summary>
        public static ValidationResult validate(Context context, String number, String productNumber, String licenseeName, ValidationParameters validationParameters)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(productNumber)) 
            {
                parameters.Add("productNumber", productNumber);
            }
            if (!String.IsNullOrEmpty(licenseeName)) 
            {
                parameters.Add("licenseeName", licenseeName);
            }

            int pmIndex = 0;
            foreach (KeyValuePair<String, Dictionary<String, String>> productModuleValidationParams in validationParameters.getParameters()) 
            {
                parameters.Add(Constants.ProductModule.PRODUCT_MODULE_NUMBER + pmIndex, productModuleValidationParams.Key);
                foreach (KeyValuePair<String, String> param in productModuleValidationParams.Value) 
                {
                    parameters.Add(param.Key + pmIndex, param.Value);
                }
                pmIndex++;
            }

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number + "/" + Constants.Licensee.ENDPOINT_PATH_VALIDATE, parameters);
            return new ValidationResult(output);
        }

        /// <summary>
        /// Transfer licenses between licensees.
        /// TODO(AY): Wiki Link
        /// </summary>
        public static void transfer(Context context, String number, String sourceLicenseeNumber)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(sourceLicenseeNumber))
            {
                parameters.Add("sourceLicenseeNumber", sourceLicenseeNumber);
            }
            NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number + "/" + Constants.Licensee.ENDPOINT_PATH_TRANSFER, parameters);
        }

    }
}
