﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the ProductModule Service. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/PaymentMethodService.html
    /// </summary>
    public class PaymentMethodService
    {

        /// <summary>
        /// Updates payment method with the given number.. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/PaymentMethodService.html
        /// </summary>
        public static PaymentMethod update(Context context, String number, PaymentMethod newPaymentMethod)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.PaymentMethod.ENDPOINT_PATH + "/" + number, newPaymentMethod.ToDictionary());
            return new PaymentMethod(output.items.item[0]);
        }

        /// <summary>
        /// Gets payment method by its number. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/PaymentMethodService.html
        /// </summary>
        public static PaymentMethod get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.PaymentMethod.ENDPOINT_PATH + "/" + number, null);
            return new PaymentMethod(output.items.item[0]);
        }

        /// <summary>
        /// Returns all payment methods. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/PaymentMethodService.html
        /// </summary>
        public static List<PaymentMethod> list(Context context)
        {

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.PaymentMethod.ENDPOINT_PATH, null);

            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    paymentMethods.Add (new PaymentMethod (i));
                }
            }
            return paymentMethods;
        }
    }
}
