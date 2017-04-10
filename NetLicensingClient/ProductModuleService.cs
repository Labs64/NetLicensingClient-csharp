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
    /// C# representation of the ProductModule Service. See NetLicensingAPI for details:
    /// https://www.labs64.de/confluence/display/NLICPUB/Product+Module+Services
    /// </summary>
    public class ProductModuleService 
    {
        /// <summary>
        /// Creates new ProductModel object with given properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Product+Module+Services#ProductModuleServices-Createproductmodule
        /// </summary>
        public static ProductModule create(Context context, String productNumber, ProductModule newProductModule)
        {
            newProductModule.productNumber = productNumber;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.ProductModule.ENDPOINT_PATH, newProductModule.ToDictionary());
            return new ProductModule(output.items.item[0]);
        }

        /// <summary>
        /// Gets product module by its number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Product+Module+Services#ProductModuleServices-Getproductmodule
        /// </summary>
        public static ProductModule get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.ProductModule.ENDPOINT_PATH + "/" + number, null);
            return new ProductModule(output.items.item[0]);
        }

        /// <summary>
        /// Returns all product modules of a vendor. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Product+Module+Services#ProductModuleServices-Productmoduleslist
        /// </summary>
        public static List<ProductModule> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter))
            {
                parameters.Add(Constants.FILTER, filter);
            } 

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.ProductModule.ENDPOINT_PATH, parameters);

            List<ProductModule> productModules = new List<ProductModule>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    productModules.Add (new ProductModule (i));
                }
            }
            return productModules;
        }

        /// <summary>
        /// Updates product module properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Product+Module+Services#ProductModuleServices-Updateproductmodule
        /// </summary>
        public static ProductModule update(Context context, String number, ProductModule updateProductModule)
        {
            updateProductModule.number = number;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.ProductModule.ENDPOINT_PATH + "/" + number, updateProductModule.ToDictionary());
            return new ProductModule(output.items.item[0]);
        }

        /// <summary>
        /// Deletes product module. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Product+Module+Services#ProductModuleServices-Deleteproductmodule
        /// </summary>
        public static void delete(Context context, String number, bool forceCascade)
        {
            String strCascade = Convert.ToString(forceCascade).ToLower();
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.ProductModule.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

    }
}
