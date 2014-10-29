using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class BasketItem
    {
        public BasketItem(Product product)
        {
            this.Product = product;
            this.DateCreated = DateTime.Now;
        }

        public readonly DateTime DateCreated;
        public readonly Product Product;
    }
}
