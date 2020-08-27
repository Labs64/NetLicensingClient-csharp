﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Contains result of the Licensee.validate() operation. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/vo/ValidationResult.html
    /// </summary>
    public class ValidationResult : IEntity
    {

        private Dictionary<String, Composition> validations;
        private DateTime ttl;

        public ValidationResult()
        {
            validations = new Dictionary<String, Composition>();
        }

        public ValidationResult(netlicensing source)
        {
            validations = new Dictionary<String, Composition>();
            ttl = source.ttl;

            if (source.items.item == null) {
                return;
            }
            foreach (item i in source.items.item)
            {
                if (!Constants.ValidationResult.VALIDATION_RESULT_TYPE.Equals(i.type))
                {
                    throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (i.type != null) ? i.type : "<null>", Constants.ValidationResult.VALIDATION_RESULT_TYPE));
                }
                Composition pmValidateProperties = new Composition();
                String productModuleNumber = null;
                if (i.property != null)
                {
                    foreach (property p in i.property)
                    {
                        switch (p.name)
                        {
                            case Constants.ProductModule.PRODUCT_MODULE_NUMBER:
                                productModuleNumber = p.Value;
                                break;
                            default:
                                pmValidateProperties.put(p.name, p.Value);
                                break;
                        }
                    }
                }
                if (i.list != null)
                {
                    foreach (list l in i.list)
                    {
                        pmValidateProperties.properties.Add(l.name, convertFromList(l));
                    }
                }
                if (productModuleNumber == null)
                {
                    throw new NetLicensingException(String.Format("Validation item does not contain property '{0}'", Constants.ProductModule.PRODUCT_MODULE_NUMBER));
                }
                setProductModuleValidation(productModuleNumber, pmValidateProperties);
            }
        }

        private Composition convertFromList(list l)
        {
            Composition result = new Composition();
            if (l.property != null)
            {
                foreach (property p in l.property)
                {
                    result.put(p.name, p.Value);
                }
            }
            if (l.list1 != null)
            {
                foreach (list l1 in l.list1)
                {
                    result.properties.Add(l1.name, convertFromList(l1));
                }
            }
            return result;
        }

        public Dictionary<String, Composition> getValidations()
        {
            return validations;
        }

        public DateTime getTtl ()
        {
	        return ttl;
        }

        public Composition getProductModuleValidation(String productModuleNumber)
        {
            try
            {
                return validations[productModuleNumber];
            }
            catch (KeyNotFoundException)
            {
                return new Composition();
            }
        }

        internal void setProductModuleValidation(String productModuleNumber, Composition productModuleValidaton)
        {
            validations.Add(productModuleNumber, productModuleValidaton);
        }

        /// <summary>
        /// Converts ValidationResult object to a String representation.  See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/vo/ValidationResult.html
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.ValidationResult.VALIDATION_RESULT_TYPE);
            sb.Append("[");
            bool first = true;
            foreach (KeyValuePair<String, Composition> validationEntry in getValidations())
            {
                if (first) {
                    first = false;
                } else {
                    sb.Append(", ");
                }
                sb.Append(Constants.ProductModule.TYPE_NAME);
                sb.Append("<");
                sb.Append(validationEntry.Key);
                sb.Append(">");
                sb.Append(validationEntry.Value.ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }

    }

    public class Composition
    {
        public Dictionary<String, Composition> properties { get; private set; }
        public String value { get; set; }

        public Composition() // list
        {
            properties = new Dictionary<String, Composition>();
            value = null;
        }

        public Composition(String value) // property
        {
            properties = null;
            this.value = value;
        }

        public void put(String key, String value) {
            properties.Add(key, new Composition(value));
        }

        public Composition this[String key]
        {
            get
            {
                return properties[key];
            }
        }

        public new String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (value == null) // list
            {
                sb.Append("{");
                if (properties == null)
                {
                    sb.Append("<null>");
                }
                else
                {
                    bool first = true;
                    foreach (KeyValuePair<String, Composition> prop in properties)
                    {
                        if (first)
                            first = false;
                        else
                            sb.Append(", ");
                        sb.Append(prop.Key);
                        sb.Append("=");
                        sb.Append(prop.Value.ToString());
                    }
                }
                sb.Append("}");
            }
            else
            {
                sb.Append(value);
            }
            return sb.ToString();
        }
    }

}
