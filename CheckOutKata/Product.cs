using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class Product
    {
        private string _productName;
        public Product(string productName)
        {
            _productName = productName;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return (obj as Product)._productName == _productName;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return _productName.GetHashCode();
        }

        public static bool operator == (Product p1, Product p2)
        {
            return (p1._productName == p2._productName);
        }

        public static bool operator !=(Product p1, Product p2)
        {
            return (p1._productName != p2._productName);
        }

        public override string ToString()
        {
            return _productName;
        }
    }
}
