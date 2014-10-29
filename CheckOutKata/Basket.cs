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

        private DiscountEngine _discountEngine;

        List<BasketItem> _basketContents = new List<BasketItem>();

        public Basket()
        {
            _discountEngine = new DiscountEngine();
        }

        public Basket(Basket oldBasket, BasketItem newBasketItem)
            : this()
        {
            _basketContents = oldBasket._basketContents;
            _basketContents.Add(newBasketItem);
        }

        internal Money GetTotalCost()
        {
            Money subTotal = new Money(0);
            foreach (BasketItem itemInBasket in _basketContents)
            {
                subTotal += _priceList[itemInBasket.Product];
            }

            return _discountEngine.ApplyDiscounts(subTotal, GetDiscountsApplicableToBasket());
        }

        private int GetProductTypeCountInBasket(Product product)
        {
            return _basketContents.Count(n => n.Equals(product));
        }

        

        private List<Product> GetUniqueProductsInBasket()
        {
            return _basketContents.Select(basketItem => basketItem.Product).Distinct().ToList();

        }
        private IList<DiscountRule> GetDiscountsApplicableToBasket()
        {
            IList<DiscountRule> applicableDiscounts = new List<DiscountRule>();
            foreach (DiscountRule productTypeDiscount in _discountEngine.GetDiscountsForProductTypesInBasket(GetUniqueProductsInBasket()))
            {
                int timesToApplyDiscount = GetNumberOfTimesToApplyDiscountForProductsInBasket(productTypeDiscount);

                for (int discountApplyCount = 0; (timesToApplyDiscount > 0) && (discountApplyCount < timesToApplyDiscount); discountApplyCount++)
                {
                    applicableDiscounts.Add(productTypeDiscount);
                }
            }

            return applicableDiscounts;
        }








    }
}
