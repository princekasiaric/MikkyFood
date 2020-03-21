using MFR.Models.Enum;
using System;
using System.Collections.Generic;

namespace MFR.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }  
        public string City { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public OrderMethod OrderMethod { get; set; }
        public DateTime OrderPlacedAt { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
