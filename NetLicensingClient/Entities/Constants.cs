using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    public class Constants
    {
        internal const String REST_API_PATH = "/core/v2/rest";
        internal const String SHOP_PATH = "/app/v2/content/shop.xhtml";
        internal const String ACTIVE = "active";
        internal const String NUMBER = "number";
        internal const String NAME = "name";
        internal const String CASCADE = "forceCascade";
        internal const String PRICE = "price";
        internal const String CURRENCY = "currency";
        internal const String FILTER = "filter";
        internal const String APIKEY_USER = "apiKey";
        internal const String TOTAL_PRICE = "totalPrice";
        internal const String AMOUNT_FIX = "amountFix";
        internal const String AMOUNT_PERCENT = "amountPercent";
        internal const String DISCOUNT = "discount";

        public class Vendor
        {
            internal const String TYPE_NAME = "Vendor";
            internal const String VENDOR_NUMBER = "vendorNumber";
        }

        public class Product
        {
            internal const String ENDPOINT_PATH = "product";
            internal const String TYPE_NAME = "Product";
            internal const String PRODUCT_NUMBER = "productNumber";
        }

        public class ProductModule
        {
            internal const String ENDPOINT_PATH = "productmodule";
            internal const String TYPE_NAME = "ProductModule";
            internal const String PRODUCT_MODULE_LICENSING_MODEL = "licensingModel";
            internal const String PRODUCT_MODULE_NUMBER = "productModuleNumber";
        }

        public class LicenseTemplate
        {
            internal const String ENDPOINT_PATH = "licensetemplate";
            internal const String TYPE_NAME = "LicenseTemplate";
            internal const String LICENSE_TEMPLATE_NUMBER = "licenseTemplateNumber";
            internal const String LICENSE_TYPE = "licenseType";
            internal const String AUTOMATIC = "automatic";
            internal const String HIDDEN = "hidden";
            internal const String HIDE_LICENSES = "hideLicenses";
        }

        public class Licensee
        {
            internal const String ENDPOINT_PATH = "licensee";
            internal const String TYPE_NAME = "Licensee";
            public const String LICENSEE_NUMBER = "licenseeNumber";
            internal const String ENDPOINT_PATH_VALIDATE = "validate";
            internal const String ENDPOINT_PATH_TRANSFER = "transfer";
            public static String PROP_LICENSEE_NAME = "licenseeName";
            public static String PROP_LICENSEE_SECRET = "licenseeSecret";
        }

        public class License
        {
            internal const String ENDPOINT_PATH = "license";
            internal const String TYPE_NAME = "License";
        }

        public class Transaction
        {
            internal const String ENDPOINT_PATH = "transaction";
            internal const String TRANSACTION_NUMBER = "transactionNumber";
            internal const String TYPE_NAME = "Transaction";
        }

        public class Token
        {
            internal const String ENDPOINT_PATH = "token";
            internal const String TYPE_NAME = "Token";
            internal const String TOKEN_NUMBER = "tokenNumber";
            public const String TOKEN_TYPE = "tokenType";
            public const String TYPE_DEFAULT = "DEFAULT";
            public const String TYPE_SHOP = "SHOP";
            public const String TYPE_APIKEY = "APIKEY";
        }

        public class PaymentMethod
        {
            internal const String ENDPOINT_PATH = "paymentmethod";
            internal const String TYPE_NAME = "PaymentMethod";
            internal const String TOKEN_NUMBER = "paymentMethodNumber";
        }

        public class Utility
        {
            internal const String ENDPOINT_PATH = "utility";
            internal const String LICENSING_MODELS = "licensingModels";
            internal const String LICENSING_MODELS_PROPERTIES = "LicensingModelProperties";
            internal const String LICENSE_TYPES = "licenseTypes";
            internal const String LICENSE_TYPE = "LicenseType";
        }

        public class ValidationResult
        {
            internal const String VALIDATION_RESULT_TYPE = "ProductModuleValidation";
        }

        public class LicensingModel
        {
            public class TryAndBuy
            {
                public const String NAME = "TryAndBuy";
            }

            public class Rental
            {
                public const String NAME = "Rental";
            }
            public class Subscription
            {
                public const String NAME = "Subscription";
            }
            public class Floating
            {
                public const String NAME = "Floating";
            }
            public class MultiFeature
            {
                public const String NAME = "MultiFeature";
            }

            public class PayPerUse
            {
                public const String NAME = "PayPerUse";
            }

            public class PricingTable
            {
                public const String NAME = "PricingTable";
            }

            public class Quota
            {
                public const String NAME = "Quota";
            }
        }
    }
}
