using AutoMapper;
using Product.API.PricingManagement.DTO.InternalApi.Request;
using Product.API.PricingManagement.DTO.InternalApi.Response;
using Product.API.PricingManagement.Infrastructure.Entities;

namespace Product.API.PricingManagement.Infrastructure.Configuration
{
    public class CustomMap:Profile
    {
        public CustomMap()
        {
            CreateMap<PriceRequest, DTO.ExternalApi.Request.PriceRequest>().ReverseMap();
            CreateMap<PriceRequest, PriceEntity>().ReverseMap();

            CreateMap<PriceResponse, DTO.ExternalApi.Response.PriceResponse>().ReverseMap();
            CreateMap<PriceResponse, PriceEntity>().ReverseMap();
        }
    }
}
