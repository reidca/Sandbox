using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class DiscountRule : IEquatable<DiscountRule>
    {
        public readonly string Name;
        public readonly List<Product> RequiredProducts;
        public readonly int RequiredPurchaseCount;
        public readonly Money Discount;

        private DiscountRule(string name, int count, Money discount)
        {
            this.Name = name;
            this.RequiredPurchaseCount = count;
            this.Discount = discount;
        }

        public DiscountRule(string name, Product requiredProduct, int requiredPurchaseCount, Money discount)
            : this(name, requiredPurchaseCount, discount)
        {
            this.RequiredProducts = new List<Product>() { requiredProduct };
        }

        public DiscountRule(string name, List<Product> requiredProducts, int requiredPurchaseCount, Money discount)
            : this(name, requiredPurchaseCount, discount)
        {
            this.RequiredProducts = requiredProducts;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return CheckForEquality((obj as DiscountRule));
        }

        private bool CheckForEquality(DiscountRule other)
        {
            return ((other.Discount == this.Discount)
                    &&
                    (other.RequiredProducts == this.RequiredProducts)
                    &&
                    (other.RequiredPurchaseCount == this.RequiredPurchaseCount));
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hashCode = this.Discount.GetHashCode()
                + this.RequiredPurchaseCount.GetHashCode();

            foreach (Product product in this.RequiredProducts)
            {
                hashCode += product.GetHashCode();
            }

            return hashCode;
        }

        public bool Equals(DiscountRule other)
        {
            return CheckForEquality(other);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
