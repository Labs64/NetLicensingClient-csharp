using System;
using System.Collections.Generic;

namespace NetLicensingClient.Entities
{
    public class ValidationParameters : IEntity
    {
        private String productNumber;
        private Boolean forOfflineUse = false;
        private Dictionary<String, String> licenseeProperties;
        private Dictionary<String, Dictionary<String, String>> parameters;

        public void setProductNumber(String productNumber)
        {
        	this.productNumber = productNumber;
        }

        public String getProductNumber ()
        {
        	return productNumber;
        }

        public void setForOfflineUse(Boolean forOfflineUse) 
        {
            this.forOfflineUse = forOfflineUse;
        }

        public Boolean isForOfflineUse() 
        {
            return forOfflineUse;
        }

        public Dictionary<String, String> getLicenseeProperties() {
            return licenseeProperties;
        }

        public void setLicenseeProperty(String key, String value) {
            licenseeProperties.Add(key, value);
        }

        public void setLicenseeName (String licenseeName)
        {
            setLicenseeProperty(Constants.Licensee.PROP_LICENSEE_NAME, licenseeName);
        }

        public String getLicenseeName ()
        {
            return licenseeProperties.TryGetValue(Constants.Licensee.PROP_LICENSEE_NAME, out string value)
                ? value
                : null;
        }

        [System.Obsolete("setLicenseeSecret() is obsolete, use NodeLocked licensing model instead")]
        public void setLicenseeSecret (String licenseeSecret)
        {
            setLicenseeProperty(Constants.Licensee.PROP_LICENSEE_SECRET, licenseeSecret);
        }

        [System.Obsolete("getLicenseeSecret() is obsolete, use NodeLocked licensing model instead")]
        public String getLicenseeSecret ()
        {
            return licenseeProperties.TryGetValue(Constants.Licensee.PROP_LICENSEE_SECRET, out string value)
                ? value
                : null;
        }

        public ValidationParameters ()
        {
            parameters = new Dictionary<String, Dictionary<String, String>>();
            licenseeProperties = new Dictionary<String, String>();
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

