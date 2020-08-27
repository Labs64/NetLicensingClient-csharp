﻿using System;
using System.Collections.Generic;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Product Service. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/ProductService.html
    /// </summary>
    public class ProductService
    {
        /// <summary>
        /// Creates new product object with given properties. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/ProductService.html
        /// </summary>
        public static Product create(Context context, Product newProduct)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Product.ENDPOINT_PATH, newProduct.ToDictionary());
            return new Product(output.items.item[0]);
        }

        /// <summary>
        /// Gets product by its number. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/ProductService.html
        /// </summary>
        public static Product get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Product.ENDPOINT_PATH + "/" + number, null);
            return new Product(output.items.item[0]);
        }

        /// <summary>
        /// Returns all products of a vendor. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/ProductService.html
        /// </summary>
        public static List<Product> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter))
            {
                parameters.Add(Constants.FILTER, filter);
            } 

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Product.ENDPOINT_PATH, parameters);

            List<Product> products = new List<Product>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    products.Add (new Product (i));
                }
            }
            return products;
        }

        /// <summary>
        /// Updates product properties. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/ProductService.html
        /// </summary>
        public static Product update(Context context, String number, Product updateProduct)
        {
            updateProduct.number = number;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Product.ENDPOINT_PATH + "/" + number, updateProduct.ToDictionary());
            return new Product(output.items.item[0]);
        }

        /// <summary>
        /// Deletes product. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/ProductService.html
        /// </summary>
        public static void delete(Context context, String number, bool forceCascade)
        {
            String strCascade = Convert.ToString(forceCascade).ToLower();
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.DELETE, Constants.Product.ENDPOINT_PATH + "/" + number, Utilities.forceCascadeToDict(forceCascade));
        }

    }
}
