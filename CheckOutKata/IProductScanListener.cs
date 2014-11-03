using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public interface ICheckoutListener
    {

        Money Total { get; set; }

    }
}
