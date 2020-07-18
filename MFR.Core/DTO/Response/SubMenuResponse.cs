namespace MFR.Core.DTO.Response
{
    public class SubMenuResponse : BaseResponse
    {
        public long SubMenuId { get; set; }
        public long MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
