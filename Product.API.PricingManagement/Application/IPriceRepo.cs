using Product.API.PricingManagement.DTO.InternalApi.Request;
using Product.API.PricingManagement.DTO.InternalApi.Response;
using Product.API.PricingManagement.Extensions.ExtraClasses;

namespace Product.API.PricingManagement.Application
{
    public interface IPriceRepo
    {
        public ApiResponse<List<PriceResponse>> GetAllPrices();
        public ApiResponse<PriceResponse> GetPriceByProductId(Guid productId);
        public ApiResponse<PriceResponse> AddPrice(PriceRequest priceRequest);
        public ApiResponse<PriceResponse> UpdatePrice(Guid productId, PriceRequest priceRequest);
    }
}
