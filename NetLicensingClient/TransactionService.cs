using System;
using System.Collections.Generic;
using NetLicensingClient.RestController;
using NetLicensingClient.Entities;

namespace NetLicensingClient
{
    /// <summary>
    /// C# representation of the Transaction Service. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/TransactionService.html
    /// </summary>
    public class TransactionService
    {

        /// <summary>
        /// Creates new transaction object with given properties. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/TransactionService.html
        /// </summary>
        public static Transaction create(Context context, Transaction newTransaction)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Transaction.ENDPOINT_PATH, newTransaction.ToDictionary());
            return new Transaction(output.items.item[0]);
        }

        /// <summary>
        /// Gets transaction by its number. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/TransactionService.html
        /// </summary>
        public static Transaction get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Transaction.ENDPOINT_PATH + "/" + number, null);
            return new Transaction(output.items.item[0]);
        }

        /// <summary>
        /// Returns all transactions of a vendor. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/TransactionService.html
        /// </summary>
        public static List<Transaction> list(Context context, String filter)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(filter))
            {
                parameters.Add(Constants.FILTER, filter);
            } 

            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Transaction.ENDPOINT_PATH, parameters);

            List<Transaction> transaction = new List<Transaction>();
            if (output.items.item != null) {
                foreach (item i in output.items.item) {
                    transaction.Add (new Transaction (i));
                }
            }
            return transaction;
        }

        /// <summary>
        /// Updates transactions properties. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/service/TransactionService.html
        /// </summary>
        public static Transaction update(Context context, String number, Transaction updateTransaction)
        {
            updateTransaction.number = number;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Transaction.ENDPOINT_PATH + "/" + number, updateTransaction.ToDictionary());
            return new Transaction(output.items.item[0]);
        }
    }
}

