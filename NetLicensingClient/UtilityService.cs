using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Utility Service.
    /// </summary>
    public class UtilityService
    {
        /// <summary>
        /// Returns all license types.
        /// </summary>
        public static List<String> listLicenseTypes(Context context)
        {

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Utility.ENDPOINT_PATH + "/" + Constants.Utility.LICENSE_TYPES , null);

            List<String> licenseTypes = new List<String>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    if (Constants.Utility.LICENSE_TYPE.Equals (i.type)) {
                        foreach (property p in i.property) {
                            if (p.name == Constants.NAME) {
                                licenseTypes.Add (p.Value);
                            }
                        }
                    }
                }
            }
            return licenseTypes;
        }

        /// <summary>
        /// Returns all licensing models.
        /// </summary>
        public static List<String> listLicensingModels(Context context)
        {

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Utility.ENDPOINT_PATH + "/" + Constants.Utility.LICENSING_MODELS, null);

            List<String> licensingModels = new List<String>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    if (Constants.Utility.LICENSING_MODELS_PROPERTIES.Equals (i.type)) {
                        foreach (property p in i.property) {
                            if (p.name == Constants.NAME) {
                                licensingModels.Add (p.Value);
                            }
                        }
                    }
                }
            }
            return licensingModels;
        }

    }
}
