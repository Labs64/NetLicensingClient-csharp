using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    public class Constants
    {
        internal const String SHOP_PATH = "/app/v2/content/shop.xhtml";
        internal const String ACTIVE = "active";
        internal const String NUMBER = "number";
        internal const String NAME = "name";
        internal const String CASCADE = "forceCascade";
        internal const String PRICE = "price";
        internal const String CURRENCY = "currency";
        internal const String FILTER = "filter";
        internal const String APIKEY_USER = "apiKey";
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

            public const String LICENSEE_AUTO_CREATE = "licenseeAutoCreate";
            public const String DESCRIPTION = "description";
            public const String LICENSING_INFO = "licensingInfo";
            public const String DISCOUNTS = "discounts";

            [System.Obsolete("Product.PROP_LICENSEE_SECRET_MODE is obsolete, use ProductModule.PROP_LICENSEE_SECRET_MODE instead")]
            public const String PROP_LICENSEE_SECRET_MODE = "licenseeSecretMode";
            public const String PROP_VAT_MODE = "vatMode";

            public class Discount 
            {
                public const String TOTAL_PRICE = "totalPrice";
                public const String AMOUNT_FIX = "amountFix";
                public const String AMOUNT_PERCENT = "amountPercent";
            }
        }

        public class ProductModule
        {
            internal const String ENDPOINT_PATH = "productmodule";
            internal const String TYPE_NAME = "ProductModule";
            internal const String PRODUCT_MODULE_LICENSING_MODEL = "licensingModel";
            internal const String PRODUCT_MODULE_NUMBER = "productModuleNumber";
            public const String PROP_LICENSEE_SECRET_MODE = "licenseeSecretMode";
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
            public const String PROP_LICENSEE_SECRET = "licenseeSecret";
        }

        public class Licensee
        {
            internal const String ENDPOINT_PATH = "licensee";
            internal const String TYPE_NAME = "Licensee";
            public const String LICENSEE_NUMBER = "licenseeNumber";
            internal const String ENDPOINT_PATH_VALIDATE = "validate";
            internal const String ENDPOINT_PATH_TRANSFER = "transfer";
            public const String PROP_LICENSEE_NAME = "licenseeName";
            [System.Obsolete("Licensee.PROP_LICENSEE_SECRET is obsolete, use License.PROP_LICENSEE_SECRET instead")]
            public const String PROP_LICENSEE_SECRET = "licenseeSecret";
            public const String PROP_MARKED_FOR_TRANSFER = "markedForTransfer";
            public const String SOURCE_LICENSEE_NUMBER = "sourceLicenseeNumber";
        }

        public class License
        {
            internal const String ENDPOINT_PATH = "license";
            public const String HIDDEN = "hidden";
            public const String LICENSE_NUMBER = "licenseNumber";
            internal const String TYPE_NAME = "License";
            public const String PROP_LICENSEE_SECRET = "licenseeSecret";
        }

        public class Transaction
        {
            internal const String ENDPOINT_PATH = "transaction";
            internal const String TRANSACTION_NUMBER = "transactionNumber";
            internal const String TYPE_NAME = "Transaction";
            public const String SOURCE_SHOP_ONLY = "shopOnly";
            public const String GRAND_TOTAL = "grandTotal";
            public const String STATUS = "status";
            public const String SOURCE = "source";
            public const String DATE_CREATED = "datecreated";
            public const String DATE_CLOSED = "dateclosed";
            public const String VAT = "vat";
            public const String VAT_MODE = "vatMode";
            public const String LICENSE_TRANSACTION_JOIN = "licenseTransactionJoin";
        }

        public class PaymentMethod
        {
            internal const String ENDPOINT_PATH = "paymentmethod";
            internal const String TYPE_NAME = "PaymentMethod";
            internal const String TOKEN_NUMBER = "paymentMethodNumber";
        }

        public class Token
        {
            internal const String ENDPOINT_PATH = "token";
            internal const String TYPE_NAME = "Token";
            internal const String TOKEN_NUMBER = "tokenNumber";
            public const String TOKEN_TYPE = "tokenType";
            public const String EXPIRATION_TIME = "expirationTime";
            public const String TOKEN_PROP_EMAIL = "email";
            public const String TOKEN_PROP_VENDORNUMBER = "vendorNumber";
            public const String TOKEN_PROP_SHOP_URL = "shopURL";

            public const String TYPE_DEFAULT = "DEFAULT";
            public const String TYPE_SHOP = "SHOP";
            public const String TYPE_APIKEY = "APIKEY";
        }

        public class Utility
        {
            internal const String ENDPOINT_PATH = "utility";
            internal const String LICENSING_MODELS = "licensingModels";
            internal const String LICENSING_MODELS_PROPERTIES = "LicensingModelProperties";
            internal const String LICENSE_TYPES = "licenseTypes";
            internal const String LICENSE_TYPE = "LicenseType";
        }

        public class Shop
        {
            public const String PROP_SHOP_LICENSE_ID = "shop-license-id";
            public const String PROP_SHOPPING_CART = "shopping-cart";
        }

        public class ValidationResult
        {
            internal const String VALIDATION_RESULT_TYPE = "ProductModuleValidation";
            public const int DEFAULT_TTL_MINUTES = 60 * 24; // 1 day
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
            public class NodeLocked
            {
                public const String NAME = "NodeLocked";
            }
        }
    }
}
