using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class Basket
    {
        private static Dictionary<Product, Money> _priceList = new Dictionary<Product, Money>() 
        {
            {new Product("A"), new Money(5)},
            {new Product("B"), new Money(10)},
            {new Product("C"), new Money(20)},

        };

        private static IList<DiscountRule> _currentDiscounts = new List<DiscountRule>()
        {
            {new DiscountRule("Buy 1 x B and 1 x C for 25", new List<Product>(){ new Product("B"), new Product("C")}, 1, new Money(5))},
            {new DiscountRule("Buy 2 x A and 2 x C for 40", new List<Product>(){ new Product("A"), new Product("C")}, 2, new Money(10))},
            {new DiscountRule("Buy 3 x A get 1 free", new Product("A"), 3, new Money(5))},
            {new DiscountRule("Buy 2 x B for 15", new Product("B"), 2, new Money(5))},
            {new DiscountRule("Buy 2 x C for 35",new Product("C"), 2, new Money(5))}

        };

        List<Product> _basketContents = new List<Product>();

        public Basket()
        {

        }

        public Basket(Basket oldBasket, Product newProduct)
        {
            _basketContents = oldBasket._basketContents;
            _basketContents.Add(newProduct);
        }

        internal Money GetTotalCost()
        {
            Money subTotal = new Money(0);
            foreach (Product addedProduct in _basketContents)
            {
                subTotal += _priceList[addedProduct];
            }

            return ApplyDiscounts(subTotal, GetDiscountsApplicableToBasket());
        }

        private int GetProductTypeCountInBasket(Product product)
        {
            return _basketContents.Count(n => n.Equals(product));
        }

        private List<Product> GetUniqueProductsInBasket()
        {
            return _basketContents.Distinct().ToList();

        }

        private int GetNumberOfTimesToApplyDiscountForProductsInBasket(DiscountRule productTypeDiscount)
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


        private IList<DiscountRule> GetDiscountsApplicableToBasket()
        {
            IList<DiscountRule> applicableDiscounts = new List<DiscountRule>();
            foreach (DiscountRule productTypeDiscount in GetDiscountsForProductTypesInBasket())
            {
                int timesToApplyDiscount = GetNumberOfTimesToApplyDiscountForProductsInBasket(productTypeDiscount);

                for (int discountApplyCount = 0; (timesToApplyDiscount > 0) && (discountApplyCount < timesToApplyDiscount); discountApplyCount++)
                {
                    applicableDiscounts.Add(productTypeDiscount);
                }
            }

            return applicableDiscounts;
        }

        private List<DiscountRule> GetDiscountsForProductTypesInBasket()
        {
            EquatableList<DiscountRule> productDiscounts = new EquatableList<DiscountRule>();

            foreach (Product productToGetDiscountRuleFor in GetUniqueProductsInBasket())
            {
                productDiscounts.AddRange(GetDiscountsApplicableToProductType(productToGetDiscountRuleFor));
            }
            return productDiscounts.Distinct().ToList();
        }

        private IList<DiscountRule> GetDiscountsApplicableToProductType(Product productToGetDiscountRuleFor)
        {
            IList<DiscountRule> productDiscounts = new List<DiscountRule>();
            foreach (DiscountRule discountRule in _currentDiscounts)
            {
                if (discountRule.RequiredProducts.Contains(productToGetDiscountRuleFor))
                {
                    productDiscounts.Add(discountRule);
                }
            }

            return productDiscounts;
        }

        private Money ApplyDiscounts(Money subTotal, IList<DiscountRule> discountsToApply)
        {
            Money discountedTotal = subTotal;

            foreach (DiscountRule discountRule in discountsToApply)
            {
                System.Diagnostics.Debug.WriteLine("Applying discount {0}", discountRule);
                discountedTotal -= discountRule.Discount;
            }

            return discountedTotal;
        }


    }
}
