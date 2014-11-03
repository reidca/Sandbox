using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class ItemScannedEventArgs : EventArgs
    {
        public ItemScannedEventArgs(Product scannedProduct, Money scannedProductPrice, Money total)
        {
            this.Total = total.ToString();
            this.ScannedProductName = scannedProduct.ToString();
            this.ScannedProductPrice = scannedProductPrice.ToString();
        }
        
        public readonly string Total;
        public readonly string ScannedProductName;
        public readonly string ScannedProductPrice;
    }
}
