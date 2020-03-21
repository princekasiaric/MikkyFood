using System;
using System.Collections.Generic;
using System.Text;

namespace MFR.Models
{
    public class SubMenu
    {
        public long Id { get; set; }
        public Menu Menu { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
