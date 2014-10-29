using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutKata
{
    public class DiscountEngine
    {

        public static IList<DiscountRule> CurrentDiscounts = new List<DiscountRule>()
        {
            {new DiscountRule("Buy 1 x B and 1 x C for 25", new List<Product>(){ new Product("B"), new Product("C")}, 1, new Money(5))},
            {new DiscountRule("Buy 2 x A and 2 x C for 40", new List<Product>(){ new Product("A"), new Product("C")}, 2, new Money(10))},
            {new DiscountRule("Buy 3 x A get 1 free", new Product("A"), 3, new Money(5))},
            {new DiscountRule("Buy 2 x B for 15", new Product("B"), 2, new Money(5))},
            {new DiscountRule("Buy 2 x C for 35",new Product("C"), 2, new Money(5))}

        };


        public Money ApplyDiscounts(Money subTotal, IList<DiscountRule> discountsToApply)
        {
            Money discountedTotal = subTotal;

            foreach (DiscountRule discountRule in discountsToApply)
            {
                System.Diagnostics.Debug.WriteLine("Applying discount {0}", discountRule);
                discountedTotal -= discountRule.Discount;
            }

            return discountedTotal;
        }

        private IList<DiscountRule> GetDiscountsApplicableToProductType(Product productToGetDiscountRuleFor)
        {
            IList<DiscountRule> productDiscounts = new List<DiscountRule>();
            foreach (DiscountRule discountRule in CurrentDiscounts)
            {
                if (discountRule.RequiredProducts.Contains(productToGetDiscountRuleFor))
                {
                    productDiscounts.Add(discountRule);
                }
            }

            return productDiscounts;
        }

        public List<DiscountRule> GetDiscountsForProductTypesInBasket(List<Product> productsInBasket)
        {
            EquatableList<DiscountRule> productDiscounts = new EquatableList<DiscountRule>();

            foreach (Product product in productsInBasket)
            {
                productDiscounts.AddRange(GetDiscountsApplicableToProductType(product));
            }
            return productDiscounts.Distinct().ToList();
        }

        public int GetNumberOfTimesToApplyDiscountForProductsInBasket(DiscountRule productTypeDiscount)
        {
            int miniumProductTypeCountInBasket = -1;

            foreach (Product requiredProduct in productTypeDiscount.RequiredProducts)
            {
                if (GetProductTypeCountInBasket(requiredProduct) >= productTypeDiscount.RequiredPurchaseCount)
                {
                    int productTypeCountInBasket = GetProductTypeCountInBasket(requiredProduct);
                    miniumProductTypeCountInBasket = ((miniumProductTypeCountInBasket == -1) ? productTypeCountInBasket : Math.Min(productTypeCountInBasket, miniumProductTypeCountInBasket));
                }
                else
                {
                    miniumProductTypeCountInBasket = -1;
                    break;
                }
            }

            return Math.Max(0, miniumProductTypeCountInBasket / productTypeDiscount.RequiredPurchaseCount);
        }
    }
}
