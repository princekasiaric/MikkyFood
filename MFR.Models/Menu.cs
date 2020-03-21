using System;
using System.Collections.Generic;

namespace MFR.Models
{
    public class Menu
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<SubMenu> SubMenus { get; set; }
    }
}
