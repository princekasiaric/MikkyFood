using System;

namespace MFR.Models
{
    public class SubMenu
    {
        public long SubMenuId { get; set; }
        public long MenuId { get; set; } 
        public Menu Menu { get; set; }  
        public string Name { get; set; } 
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
