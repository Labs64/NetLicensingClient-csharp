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
    /// C# representation of the Transaction Service. See NetLicensingAPI for details:
    /// https://www.labs64.de/confluence/display/NLICPUB/Transaction+Services
    /// </summary>
    public class TransactionService
    {

        /// <summary>
        /// Creates new transaction object with given properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Transaction+Services#TransactionServices-Createtransaction
        /// </summary>
        public static Transaction create(Context context, Transaction newTransaction)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Transaction.ENDPOINT_PATH, newTransaction.ToDictionary());
            return new Transaction(output.items.item[0]);
        }

        /// <summary>
        /// Gets transaction by its number. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Transaction+Services#TransactionServices-Gettransaction
        /// </summary>
        public static Transaction get(Context context, String number)
        {
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.GET, Constants.Transaction.ENDPOINT_PATH + "/" + number, null);
            return new Transaction(output.items.item[0]);
        }

        /// <summary>
        /// Returns all transactions of a vendor. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Transaction+Services#TransactionServices-Transactionslist
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
        /// Updates transactions properties. See NetLicensingAPI for details:
        /// https://www.labs64.de/confluence/display/NLICPUB/Transaction+Services#TransactionServices-Updatetransaction
        /// </summary>
        public static Transaction update(Context context, String number, Transaction updateTransaction)
        {
            updateTransaction.number = number;
            netlicensing output = NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Transaction.ENDPOINT_PATH + "/" + number, updateTransaction.ToDictionary());
            return new Transaction(output.items.item[0]);
        }
    }
}

