﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOutKata
{
    public interface IBasketItem
    {
        DateTime DateCreated { get; }
        Product Product { get; }
        bool HasBeenDiscounted { get; }
        string DiscountApplied { get; }
    }
}
