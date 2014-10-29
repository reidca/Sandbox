using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class CheckOut
    {
        private Basket _currentBasket;

        public CheckOut()
        {
            _currentBasket = new Basket();
        }

        private CheckOut(Basket basket)
        {
            _currentBasket = basket;
        }


        internal Money GetTotal()
        {
            return _currentBasket.GetTotalCost();
        }

        internal CheckOut Scan(Product scannedProduct)
        {
            return new CheckOut(new Basket(_currentBasket, new BasketItem(scannedProduct)));
        }
    }
}
