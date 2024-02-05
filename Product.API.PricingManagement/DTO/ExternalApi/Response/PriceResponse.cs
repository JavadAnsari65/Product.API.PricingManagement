namespace Product.API.PricingManagement.DTO.ExternalApi.Response
{
    public class PriceResponse
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public decimal DiscountPercentage { get; set; } = 0;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
