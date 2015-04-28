using System;
using System.Collections.Generic;

namespace NetLicensingClient.Entities
{
    public class ValidationParameters : IEntity
    {
        
        private Dictionary<String, Dictionary<String, String>> parameters;

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

