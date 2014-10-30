using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutKata
{
    public class DiscountEngine
    {
        private Basket _basket;

        public DiscountEngine(Basket basket)
        {
            _basket = basket;
        }

        public static IList<DiscountRule> CurrentDiscounts = new List<DiscountRule>()
        {
            {new DiscountRule("Buy 1 x B and 1 x C for 25", new List<Product>(){ new Product("B"), new Product("C")}, 1, new Money(5))},
            {new DiscountRule("Buy 2 x A and 2 x C for 40", new List<Product>(){ new Product("A"), new Product("C")}, 2, new Money(10))},
            {new DiscountRule("Buy 3 x A get 1 free", new Product("A"), 3, new Money(5))},
            {new DiscountRule("Buy 2 x B for 15", new Product("B"), 2, new Money(5))},
            {new DiscountRule("Buy 2 x C for 35",new Product("C"), 2, new Money(5))}

        };


        public Money ApplyDiscounts()
        {
            Money discountedTotal = _basket.SubTotal();

            foreach (DiscountRule discountRule in GetDiscountsApplicableToBasket())
            {
                System.Diagnostics.Debug.WriteLine("Applying discount {0}", discountRule);
                discountedTotal -= discountRule.Discount;
            }

            return discountedTotal;
        }

        private static IList<DiscountRule> GetDiscountsApplicableToProductType(Product productToGetDiscountRuleFor)
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

        private static List<DiscountRule> GetDiscountsForProductTypesInBasket(List<Product> productsInBasket)
        {
            EquatableList<DiscountRule> productDiscounts = new EquatableList<DiscountRule>();

            foreach (Product product in productsInBasket)
            {
                productDiscounts.AddRange(GetDiscountsApplicableToProductType(product));
            }
            return productDiscounts.Distinct().ToList();
        }

        private IList<DiscountRule> GetDiscountsApplicableToBasket()
        {
            IList<DiscountRule> applicableDiscounts = new List<DiscountRule>();

            IList<IBasketItem> basketItemsNotCurrentlyDiscounted = GetBasketItemsWhereNoDiscountHasBeenApplied(_basket.BasketContents);

            foreach (DiscountRule productTypeDiscount in GetDiscountsForProductTypesInBasket(_basket.GetUniqueProductsInBasket()))
            {
                int timesToApplyDiscount = GetNumberOfTimesToApplyDiscountFoSelectedProducts(basketItemsNotCurrentlyDiscounted.Select(basketItem => basketItem.Product).ToList(), productTypeDiscount);

                if (timesToApplyDiscount > 0)
                {
                    for (int discountApplyCount = 0; (timesToApplyDiscount > 0) && (discountApplyCount < timesToApplyDiscount); discountApplyCount++)
                    {
                        applicableDiscounts.Add(productTypeDiscount);
                    }

                    basketItemsNotCurrentlyDiscounted = GetBasketItemsWhereNoDiscountHasBeenApplied(UpdateDiscountStatus(basketItemsNotCurrentlyDiscounted, productTypeDiscount));
                }
            }

            return applicableDiscounts;
        }

        private static IList<IBasketItem> GetBasketItemsWhereNoDiscountHasBeenApplied(IList<IBasketItem> basketItems)
        {
            IList<IBasketItem> basketItemsNotCurrentlyDiscounted = basketItems.Where(basketItem => basketItem.HasBeenDiscounted == false).ToList();
            return basketItemsNotCurrentlyDiscounted;
        }

        private static IList<IBasketItem> UpdateDiscountStatus(IList<IBasketItem> basketItems, DiscountRule productTypeDiscountApplied)
        {
            int numberOfEachProductToRemove = GetQualifyingProductCountForDiscount(basketItems.Select(basketItem => basketItem.Product).ToList(), productTypeDiscountApplied);
            IList<IBasketItem> postDiscountBasketItems = new List<IBasketItem>();
            foreach (Product requiredProduct in productTypeDiscountApplied.RequiredProducts)
            {
                int quantityRemoved = 0;
                foreach (BasketItem basketItem in basketItems)
                {
                    if ((basketItem.Product == requiredProduct) && (quantityRemoved <= numberOfEachProductToRemove))
                    {
                        quantityRemoved += 1;
                        
                        postDiscountBasketItems.Add(new DiscountedBasketItem(basketItem,productTypeDiscountApplied ));
                    }
                }
            }

            return postDiscountBasketItems;
        }
        
        private static int GetProductTypeCountInSelectedProducts(IList<Product> selectedProducts, Product productType)
        {
            return selectedProducts.Count(n => n.Equals(productType));
        }

        private static int GetNumberOfTimesToApplyDiscountFoSelectedProducts(IList<Product> selectedProducts, DiscountRule productTypeDiscount)
        {
            int miniumProductTypeCountInSelectedProducts = GetQualifyingProductCountForDiscount(selectedProducts, productTypeDiscount);

            return Math.Max(0, miniumProductTypeCountInSelectedProducts / productTypeDiscount.RequiredPurchaseCount);
        }

        private static int GetQualifyingProductCountForDiscount(IList<Product> selectedProducts, DiscountRule productTypeDiscount)
        {
            int miniumProductTypeCountInSelectedProducts = -1;

            foreach (Product requiredProduct in productTypeDiscount.RequiredProducts)
            {
                int productTypeCountInSelectedProducts = GetProductTypeCountInSelectedProducts(selectedProducts, requiredProduct);

                if (productTypeCountInSelectedProducts >= productTypeDiscount.RequiredPurchaseCount)
                {
                    miniumProductTypeCountInSelectedProducts = ((miniumProductTypeCountInSelectedProducts == -1) ? productTypeCountInSelectedProducts : Math.Min(productTypeCountInSelectedProducts, miniumProductTypeCountInSelectedProducts));
                }
                else
                {
                    miniumProductTypeCountInSelectedProducts = -1;
                    break;
                }
            }
            return miniumProductTypeCountInSelectedProducts;
        }
    }
}
