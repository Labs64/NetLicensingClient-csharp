using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents Product. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Product.html
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Product name. Not null. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Product.html
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// Custom properties of the product. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/Product.html
        /// </summary>
        public Dictionary<String, String> productProperties { get; private set; }

        private List<ProductDiscount> productDiscounts { get; set; }

        private Boolean productDiscountsTouched = false;

        // default constructor
        public Product()
        {
            productProperties = new Dictionary<String, String>();
            productDiscounts = new List<ProductDiscount>();
        }

        // construct from REST response item
        internal Product(item source)
        {
            if (!Constants.Product.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.Product.TYPE_NAME));
            }
            productProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                switch (p.name)
                {
                    case Constants.NAME:
                        name = p.Value;
                        break;
                    default:
                        if (!base.setFromProperty(p)) // Not BaseEntity property?
                        {
                            // custom property
                            if (!productProperties.ContainsKey(p.name))
                            {
                                productProperties.Add(p.name, p.Value);
                            }
                        }
                        break;
                }
            }

            //add discounts
            productDiscounts = new List<ProductDiscount>();

            if (source.list != null)
            {
                foreach (list list in source.list)
                {
                    productDiscounts.Add(new ProductDiscount(list));
                }
            }
        }

        public List<ProductDiscount> getProductDiscounts()
        {
            return productDiscounts;
        }

        public void setProductDiscounts(List<ProductDiscount> productDiscounts)
        {

            foreach (ProductDiscount discount in productDiscounts)
            {
                discount.setProduct(this);
            }

            this.productDiscounts = productDiscounts;
            productDiscountsTouched = true;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.Product.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            foreach (KeyValuePair<String, String> prop in productProperties)
            {
                sb.Append(", ");
                sb.Append(prop.Key);
                sb.Append("=");
                sb.Append(prop.Value);
            }

            if (productDiscounts.Count > 0)
            {
                foreach (ProductDiscount discount in productDiscounts)
                {
                    sb.Append(", ");
                    sb.Append("discount");
                    sb.Append("=");
                    sb.Append(discount.ToString());
                }
            }
            else
            {
                if (productDiscountsTouched)
                {
                    sb.Append(", ");
                    sb.Append("discount");
                    sb.Append("=");
                }
            }

            sb.Append("]");
            return sb.ToString();
        }

        internal new Dictionary<String, String> ToDictionary()
        {
            Dictionary<String, String> dict = base.ToDictionary();
            if (name != null) dict[Constants.NAME] = name;
            foreach (KeyValuePair<String, String> prop in productProperties)
            {
                dict[prop.Key] = prop.Value;
            }

            if (productDiscounts.Count > 0)
            {
                for (int i = 0; i < productDiscounts.Count; i++)
                {
                    ProductDiscount discount = productDiscounts.ElementAt(i);
                    dict[Constants.DISCOUNT + "[" + i + "]"] = discount.ToString();
                }
            }
            else
            {
                if (productDiscountsTouched)
                {
                    dict[Constants.DISCOUNT] = "";
                }
            }

            return dict;
        }

    }
}
