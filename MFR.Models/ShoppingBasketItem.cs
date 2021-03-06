﻿using System;

namespace MFR.DomainModels
{
    public class ShoppingBasketItem
    {
        public long Id { get; set; }
        public SubMenu SubMenu { get; set; }
        public string ShoppingBasketId { get; set; }
        public int Quantity { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
