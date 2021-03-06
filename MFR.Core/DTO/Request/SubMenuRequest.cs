﻿namespace MFR.Core.DTO.Request
{
    public class SubMenuRequest : BaseRequest
    {
        public long MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
