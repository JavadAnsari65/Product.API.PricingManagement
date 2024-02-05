using Product.API.PricingManagement.Extensions.ExtraClasses;
using Product.API.PricingManagement.Infrastructure.Configuration;
using Product.API.PricingManagement.Infrastructure.Entities;

namespace Product.API.PricingManagement.Infrastructure.Repository
{
    public class CRUDService
    {
        private readonly PricingDbContext _pricingDbContext;

        public CRUDService(PricingDbContext pricingDbContext)
        {
            _pricingDbContext = pricingDbContext;
        }

        public ApiResponse<List<PriceEntity>> GetAllPricesOfDB()
        {
            try
            {
                var getPrices = _pricingDbContext.Prices.ToList();

                return new ApiResponse<List<PriceEntity>>
                {
                    Result = true,
                    Data = getPrices
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<PriceEntity>>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<PriceEntity> GetPriceByProductIdOfDB(Guid productId)
        {
            try
            {
                var getPrice = _pricingDbContext.Prices.FirstOrDefault(x => x.ProductId == productId);

                return new ApiResponse<PriceEntity>
                {
                    Result = true,
                    Data = getPrice
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PriceEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<PriceEntity> AddPriceInDB(PriceEntity price)
        {
            try
            {
                _pricingDbContext.Add(price);
                _pricingDbContext.SaveChanges();

                return new ApiResponse<PriceEntity>
                {
                    Result = true,
                    Data = price
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PriceEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<PriceEntity> UpdatePriceInDB(Guid productId, PriceEntity priceRequest)
        {
            try
            {
                var existPrice = _pricingDbContext.Prices.FirstOrDefault(x=>x.ProductId == productId);

                if(existPrice != null)
                {
                    existPrice.Price = priceRequest.Price;
                    existPrice.Currency = priceRequest.Currency;
                    existPrice.DiscountPercentage = priceRequest.DiscountPercentage;
                    existPrice.StartDate = priceRequest.StartDate;
                    existPrice.EndDate = priceRequest.EndDate;

                    _pricingDbContext.SaveChanges();

                    return new ApiResponse<PriceEntity>
                    {
                        Result = true,
                        Data = existPrice
                    };
                }
                else
                {
                    return new ApiResponse<PriceEntity>
                    {
                        Result = false,
                        ErrorMessage = "The Price is not found!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<PriceEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
