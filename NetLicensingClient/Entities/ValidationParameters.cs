using System;
using System.Collections.Generic;

namespace NetLicensingClient.Entities
{
    public class ValidationParameters : IEntity
    {
        private String productNumber;
        private String licenseeName;
        private String licenseeSecret;
        private Dictionary<String, Dictionary<String, String>> parameters;

        public void setProductNumber(String productNumber)
        {
        	this.productNumber = productNumber;
        }

        public String getProductNumber ()
        {
        	return productNumber;
        }

        public void setLicenseeName (String licenseeName)
        {
            this.licenseeName = licenseeName;
        }

        public String getLicenseeName ()
        {
    	    return licenseeName;
        }

        public void setLicenseeSecret (String licenseeSecret)
        {
            this.licenseeSecret = licenseeSecret;
        }

        public String getLicenseeSecret ()
        {
        	return licenseeSecret;
        }

        public ValidationParameters ()
        {
            parameters = new Dictionary<String, Dictionary<String, String>>();
        }

        public Dictionary<String, Dictionary<String, String>> getParameters()
        {
            return parameters;
        }

        public Dictionary<String, String> getProductModuleValidationParameters(String productModuleNumber) {
            try
            {
                return parameters[productModuleNumber];
            }
            catch (KeyNotFoundException)
            {
                Dictionary<String, String> productModuleValidationParametes = new Dictionary<String, String> ();
                parameters.Add(productModuleNumber, productModuleValidationParametes);
                return productModuleValidationParametes;
            }
        }

        internal void setProductModuleValidation(String productModuleNumber, Dictionary<String, String> productModuleValidationParametes)
        {
            parameters.Add(productModuleNumber, productModuleValidationParametes);
        }

        public void put(String productModuleNumber, String key, String value) 
        {
            getProductModuleValidationParameters(productModuleNumber).Add(key, value);
        }
    }
}

