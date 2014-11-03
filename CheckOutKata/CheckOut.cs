using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class CheckOut
    {
        public event ItemScannedEventHandler ItemScanned;
        public event TotalCalculatedEventHandler TotalCalculated;
        public delegate void ItemScannedEventHandler(object sender, ItemScannedEventArgs e);
        public delegate void TotalCalculatedEventHandler(object sender, TotalCalculatedEventArgs e);



        private Basket _currentBasket;

        public CheckOut()
        {
            _currentBasket = new Basket();
        }

        private CheckOut(Basket basket, ItemScannedEventHandler itemScannedHandler, TotalCalculatedEventHandler totalCalculatedHandler)
        {
            _currentBasket = basket;
            this.ItemScanned = itemScannedHandler;
            this.TotalCalculated = totalCalculatedHandler;
        }

        internal void CalculateTotal()
        {
            if (this.TotalCalculated != null)
            {
                this.TotalCalculated(this, new TotalCalculatedEventArgs(_currentBasket.GetTotalCost()));
            }
        }

        internal CheckOut Scan(Product scannedProduct)
        {
            var b = new Basket(_currentBasket, new BasketItem(scannedProduct));
            var c = new CheckOut(b,this.ItemScanned, this.TotalCalculated);
            
            if (this.ItemScanned != null)
            {
                this.ItemScanned(this, new ItemScannedEventArgs(scannedProduct,PricingEngine.PriceList[scannedProduct],b.GetTotalCost()));
            }

            return c;

            
        }
    }
}
