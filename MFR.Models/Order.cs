using MFR.DomainModels.Enum;
using System;
using System.Collections.Generic;

namespace MFR.DomainModels
{
    public class Order
    {
        public long OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }  
        public string Email { get; set; }
        public string Address { get; set; }  
        public string Location { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public OrderMethod OrderMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime OrderPlacedAt { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public Reservation Reservation { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
