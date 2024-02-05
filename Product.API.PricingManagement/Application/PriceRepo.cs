using AutoMapper;
using Product.API.PricingManagement.DTO.InternalApi.Request;
using Product.API.PricingManagement.DTO.InternalApi.Response;
using Product.API.PricingManagement.Extensions.ExtraClasses;
using Product.API.PricingManagement.Infrastructure.Entities;
using Product.API.PricingManagement.Infrastructure.Repository;

namespace Product.API.PricingManagement.Application
{
    public class PriceRepo:IPriceRepo
    {
        private readonly IMapper _mapper;
        private readonly CRUDService _crudService;

        public PriceRepo(IMapper mapper, CRUDService crudService)
        {
            _mapper = mapper;
            _crudService = crudService;
        }

        public ApiResponse<List<PriceResponse>> GetAllPrices()
        {
            try
            {
                var getPrices = _crudService.GetAllPricesOfDB();

                if (getPrices.Result)
                {
                    var mapPricesResponse = _mapper.Map<List<PriceResponse>>(getPrices.Data);

                    var dateNow = DateTime.Now;
                    foreach ( var item in mapPricesResponse )
                    {
                        if (item.DiscountPercentage > 0)
                        {
                            if (dateNow >= item.StartDate && dateNow <= item.EndDate)
                            {
                                item.Price = item.Price - ((item.Price * item.DiscountPercentage) / 100);
                            }
                        }
                    }

                    return new ApiResponse<List<PriceResponse>>
                    {
                        Result = getPrices.Result,
                        Data = mapPricesResponse
                    };
                }
                else
                {
                    return new ApiResponse<List<PriceResponse>>
                    {
                        Result = getPrices.Result,
                        ErrorMessage = getPrices.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<PriceResponse>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<PriceResponse> GetPriceByProductId(Guid productId)
        {
            try
            {
                var getPrice = _crudService.GetPriceByProductIdOfDB(productId);

                if (getPrice.Result)
                {
                    var mapPriceResponse = _mapper.Map<PriceResponse>(getPrice.Data);

                    if(mapPriceResponse.DiscountPercentage > 0)
                    {
                        var dateNow = DateTime.Now;
                        if(dateNow >= mapPriceResponse.StartDate && dateNow <= mapPriceResponse.EndDate)
                        {
                            mapPriceResponse.Price = mapPriceResponse.Price - ((mapPriceResponse.Price * mapPriceResponse.DiscountPercentage) / 100);
                        }
                    }

                    return new ApiResponse<PriceResponse>
                    {
                        Result = getPrice.Result,
                        Data = mapPriceResponse
                    };
                }
                else
                {
                    return new ApiResponse<PriceResponse>
                    {
                        Result = getPrice.Result,
                        ErrorMessage = getPrice.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<PriceResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<PriceResponse> AddPrice(PriceRequest priceRequest)
        {
            try
            {
                if (priceRequest!=null)
                {
                    var mapPriceEntity = _mapper.Map<PriceEntity>(priceRequest);
                    var addPrice = _crudService.AddPriceInDB(mapPriceEntity);

                    if (addPrice.Result)
                    {
                        var mapPriceResponse = _mapper.Map<PriceResponse>(addPrice.Data);

                        return new ApiResponse<PriceResponse>
                        {
                            Result = addPrice.Result,
                            Data = mapPriceResponse
                        };
                    }
                    else
                    {
                        return new ApiResponse<PriceResponse>
                        {
                            Result = addPrice.Result,
                            ErrorMessage = addPrice.ErrorMessage
                        };
                    }
                }
                else
                {
                    return new ApiResponse<PriceResponse>
                    {
                        Result = false,
                        ErrorMessage = "The User is null!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<PriceResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<PriceResponse> UpdatePrice(Guid productId, PriceRequest priceRequest)
        {
            try
            {
                var mapPriceEntity = _mapper.Map<PriceEntity>(priceRequest);
                var updatedPrice = _crudService.UpdatePriceInDB(productId, mapPriceEntity);

                if(updatedPrice.Result)
                {
                    var mapPriceResponse = _mapper.Map<PriceResponse>(updatedPrice.Data);

                    if (mapPriceResponse.DiscountPercentage > 0)
                    {
                        var dateNow = DateTime.Now;
                        if (dateNow >= mapPriceResponse.StartDate && dateNow <= mapPriceResponse.EndDate)
                        {
                            mapPriceResponse.Price = mapPriceResponse.Price - ((mapPriceResponse.Price * mapPriceResponse.DiscountPercentage) / 100);
                        }
                    }

                    return new ApiResponse<PriceResponse>
                    {
                        Result = updatedPrice.Result,
                        Data = mapPriceResponse
                    };
                }
                else
                {
                    return new ApiResponse<PriceResponse>
                    {
                        Result = updatedPrice.Result,
                        ErrorMessage = updatedPrice.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<PriceResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
