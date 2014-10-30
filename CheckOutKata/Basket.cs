using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class Basket
    {
        internal List<IBasketItem> BasketContents = new List<IBasketItem>();

        public Basket()
        {

        }

        public Basket(Basket oldBasket, BasketItem newBasketItem)
            : this()
        {
            this.BasketContents = oldBasket.BasketContents;
            this.BasketContents.Add(newBasketItem);
        }

        internal Money SubTotal()
        {
            Money subTotal = new Money(0);
            foreach (BasketItem itemInBasket in BasketContents)
            {
                subTotal += PricingEngine.PriceList[itemInBasket.Product];
            }

            return subTotal;
        }

        public Money GetTotalCost()
        {
            return new DiscountEngine(this).ApplyDiscounts();
        }

        internal List<Product> GetUniqueProductsInBasket()
        {
            return this.BasketContents.Select(basketItem => basketItem.Product).Distinct().ToList();
        }
    }
}
