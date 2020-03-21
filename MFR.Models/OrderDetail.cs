using System;
using System.Collections.Generic;
using System.Text;

namespace MFR.Models
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public Order Order { get; set; }
        public SubMenu SubMenu { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
