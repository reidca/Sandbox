using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    
    public class BasketItem : IBasketItem
    {

        Product _product;
        DateTime _dateCreated;
        bool _hasBeenDiscounted;
        string _discountApplied;
        
        public BasketItem(Product product)
        {
            _product = product;
            this._dateCreated = DateTime.Now;
            this._hasBeenDiscounted = false;
            this._discountApplied = string.Empty;
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
        }

        public Product Product
        {
            get { return _product; }
        }

        public bool HasBeenDiscounted
        {
            get { return _hasBeenDiscounted; }
        }

        public string DiscountApplied
        {
            get { return _discountApplied; }
        }
    }
}
