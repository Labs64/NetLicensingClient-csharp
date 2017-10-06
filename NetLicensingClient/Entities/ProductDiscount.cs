using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient.RestController;

namespace NetLicensingClient.Entities
{
    public class ProductDiscount : BaseEntity
    {
        private Product product;

        public Decimal totalPrice { get; set; }

        public String currency { get; set; }

        private Decimal _amountFix = 0;

        public Decimal amountFix
        {
            get
            {
                return _amountFix;
            }
            set
            {
                _amountPercent = 0;
                _amountFix = value;
            }
        }

        private int _amountPercent = 0;

        public int amountPercent
        {
            get
            {
                return _amountPercent;
            }
            set
            {
                _amountFix = 0;
                _amountPercent = value;
            }
        }
        public ProductDiscount setProduct(Product product)
        {
            this.product = product;
            return this;
        }

        public Product getProduct()
        {
            return product;
        }

        public ProductDiscount setTotalPrice(Decimal totalPrice)
        {
            this.totalPrice = totalPrice;

            return this;
        }

        public Decimal getTotalPrice()
        {
            return totalPrice;
        }

        public ProductDiscount setCurrency(String currency)
        {
            this.currency = currency;

            return this;
        }

        public String getCurrency()
        {
            return currency;
        }

        public ProductDiscount setAmountFix(Decimal amountFix)
        {
            _amountPercent = 0;
            _amountFix = amountFix;

            return this;
        }

        public Decimal getAmountFix()
        {
            return _amountFix;
        }

        public ProductDiscount setAmountPercent(int amountPercent)
        {
            _amountFix = 0;
            _amountPercent = amountPercent;

            return this;
        }

        public int getAmountPercent()
        {
            return _amountPercent;
        }

        public ProductDiscount()
        {
        }

        internal ProductDiscount(list source)
        {

            foreach (property p in source.property)
            {
                switch (p.name)
                {
                    case Constants.TOTAL_PRICE:
                        totalPrice = Utilities.CheckedParseDecimal(p.Value, Constants.TOTAL_PRICE);
                        break;
                    case Constants.CURRENCY:
                        currency = p.Value;
                        break;
                    case Constants.AMOUNT_FIX:
                        _amountFix = Utilities.CheckedParseDecimal(p.Value, Constants.AMOUNT_FIX);
                        break;
                    case Constants.AMOUNT_PERCENT:
                        _amountPercent = Int16.Parse(p.Value);
                        break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Decimal amount = 0;

            sb.Append(totalPrice.ToString("F2", Utilities.NetLicensingNumberFormat));
            sb.Append(";");

            sb.Append(currency);
            sb.Append(";");

            if (_amountFix > 0)
            {
                sb.Append(_amountFix.ToString("F2", Utilities.NetLicensingNumberFormat));
            }

            if (_amountPercent > 0)
            {
                sb.Append(_amountPercent);
                sb.Append("%");
            }

            return sb.ToString();
        }
    }
}
