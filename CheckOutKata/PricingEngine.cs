using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutKata
{
    public static class PricingEngine
    {
        internal static Dictionary<Product, Money> PriceList;

        static PricingEngine()
        {
            PriceList = new Dictionary<Product, Money>() 
            {
                {new Product("A"), new Money(5)},
                {new Product("B"), new Money(10)},
                {new Product("C"), new Money(20)},
                {new Product("D"), new Money(50)}
            };
        }

    }
}
