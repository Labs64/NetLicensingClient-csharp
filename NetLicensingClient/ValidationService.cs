using NetLicensingClient.Entities;
using NetLicensingClient.RestController;
using NetLicensingClient.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NetLicensingClient
{
    public class ValidationService
    {
        public static ValidationResult validate(Context context, String number, ValidationParameters validationParameters, int timeoutInMilliseconds = 100000)
        {
            return convertValidationResult(retrieveValidationFile(context, number, validationParameters, timeoutInMilliseconds));
        }

        public static ValidationResult validateOffline(Context context, string validationFile)
        {
            if (!SignatureUtils.check(validationFile, context.publicKey)) 
            {
                throw new NetLicensingException("XML signature could not be verified");
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(validationFile);
            MemoryStream stream = new MemoryStream(byteArray);

            XmlSerializer serializer = new XmlSerializer(typeof(netlicensing));
            netlicensing response = (netlicensing)serializer.Deserialize(stream);

            return convertValidationResult(response);
        }

        internal static netlicensing retrieveValidationFile(Context context, String number, ValidationParameters validationParameters, int timeoutInMilliseconds = 100000)
        {
            Dictionary<String, String> parameters = convertValidationParameters(validationParameters);

            return NetLicensingAPI.request(context, NetLicensingAPI.Method.POST, Constants.Licensee.ENDPOINT_PATH + "/" + number + "/" + Constants.Licensee.ENDPOINT_PATH_VALIDATE, parameters, timeoutInMilliseconds);
        }

        internal static ValidationResult convertValidationResult(netlicensing validationFile)
        {
            return (validationFile != null) ? new ValidationResult(validationFile) : null;
        }

        internal static Dictionary<String, String> convertValidationParameters(ValidationParameters validationParameters)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            if (!String.IsNullOrEmpty(validationParameters.getProductNumber()))
            {
                parameters.Add(Constants.Product.PRODUCT_NUMBER, validationParameters.getProductNumber());
            }

            if (validationParameters.isForOfflineUse())
            {
                parameters.Add(Constants.Validation.FOR_OFFLINE_USE, "true");
            }

            foreach (KeyValuePair<String, String> property in validationParameters.getLicenseeProperties())
            {
                if (!String.IsNullOrEmpty(property.Key))
                {
                    parameters.Add(property.Key, property.Value);
                }
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

            return parameters;
        }
    }
}
