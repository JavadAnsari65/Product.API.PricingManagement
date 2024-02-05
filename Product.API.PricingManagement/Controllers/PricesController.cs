using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.PricingManagement.Application;
using Product.API.PricingManagement.DTO.InternalApi.Request;
using Product.API.PricingManagement.DTO.InternalApi.Response;

namespace Product.API.PricingManagement.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPriceRepo _priceRepo;

        public PricesController(IMapper mapper, IPriceRepo priceRepo)
        {
            _mapper = mapper;
            _priceRepo = priceRepo;
        }

        [HttpGet]
        public ActionResult<List<DTO.ExternalApi.Response.PriceResponse>> GetAllPrices()
        {
            try
            {
                var getPrices = _priceRepo.GetAllPrices();

                if (getPrices.Result)
                {
                    var mapPicesExternal = _mapper.Map<List<DTO.ExternalApi.Response.PriceResponse>>(getPrices.Data);

                    return Ok(mapPicesExternal);
                }
                else
                {
                    return BadRequest(getPrices.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<DTO.ExternalApi.Response.PriceResponse> GetPriceByProductId(Guid productId)
        {
            try
            {
                var getPrice = _priceRepo.GetPriceByProductId(productId);

                if(getPrice.Result)
                {
                    var mapPriceExternal = _mapper.Map<DTO.ExternalApi.Response.PriceResponse>(getPrice.Data);
                    return Ok(mapPriceExternal);
                }
                else
                {
                    return BadRequest(getPrice.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<DTO.ExternalApi.Response.PriceResponse> AddPrice(DTO.ExternalApi.Request.PriceRequest priceRequest)
        {
            try
            {
                var mapPriceInternal = _mapper.Map<PriceRequest>(priceRequest);
                var addPrice = _priceRepo.AddPrice(mapPriceInternal);

                if (addPrice.Result)
                {
                    var mapPriceExternal = _mapper.Map<DTO.ExternalApi.Response.PriceResponse>(addPrice.Data);

                    return Ok(mapPriceExternal);
                }
                else
                {
                    return BadRequest(addPrice.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<DTO.ExternalApi.Response.PriceResponse> UpdatePrice(Guid productId, DTO.ExternalApi.Request.PriceRequest priceRequest)
        {
            try
            {
                if(priceRequest.ProductId == productId)
                {
                    var mapPriceInternal = _mapper.Map<PriceRequest>(priceRequest);
                    var updatedPrice = _priceRepo.UpdatePrice(productId, mapPriceInternal);

                    if(updatedPrice.Result)
                    {
                        var mapPriceExternal = _mapper.Map<DTO.ExternalApi.Response.PriceResponse>(updatedPrice.Data);
                        return Ok(mapPriceExternal);
                    }
                    else
                    {
                        return BadRequest(updatedPrice.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest("The ProductId is not match!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
