namespace MFR.Core.DTO.Response
{
    public class ShoppingBasketResponse : BaseResponse
    {
        public long Id { get; set; }
        public int Quantity { get; set; }

        public string ShoppingBasketId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public decimal ShoppingBasketTotal { get; set; }
        public decimal VAT { get; set; }
    }
}
