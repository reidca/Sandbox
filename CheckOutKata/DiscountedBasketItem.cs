using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutKata
{
    public class DiscountedBasketItem : IBasketItem
    {

        Product _product;
        DateTime _dateCreated;
        bool _hasBeenDiscounted;
        string _discountApplied;

        public DiscountedBasketItem(IBasketItem undiscountedBasketItem, DiscountRule discountApplied)
        {
            this._product = undiscountedBasketItem.Product;
            this._dateCreated = undiscountedBasketItem.DateCreated;
            this._hasBeenDiscounted = true;
            this._discountApplied = discountApplied.Name;
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
