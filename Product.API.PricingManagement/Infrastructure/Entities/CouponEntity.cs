using System.ComponentModel.DataAnnotations;

namespace Product.API.PricingManagement.Infrastructure.Entities
{
    public class CouponEntity
    {
        public int CouponId { get; set; }
        [Key]
        public string Coupon { get; set; }
        public string DiscountType { get; set; } = "Percentage"; // "Amount" or "Percentage"
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
