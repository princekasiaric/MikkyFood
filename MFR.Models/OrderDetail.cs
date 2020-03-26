using System;

namespace MFR.DomainModels
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long SubMenuId { get; set; }  
        public SubMenu SubMenu { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
