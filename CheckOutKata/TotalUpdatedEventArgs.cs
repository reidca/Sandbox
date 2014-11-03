using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutKata
{
    public class TotalCalculatedEventArgs : EventArgs
    {
        public TotalCalculatedEventArgs(Money total)
        {
            this.Total = total;
        }
        public Money Total;
    }
}
