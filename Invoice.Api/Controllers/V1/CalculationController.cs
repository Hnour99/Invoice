using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.DataTransferObject.DTOs.Item;
using Invoice.DataTransferObject.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [HelperClasses.Authorize]
    public class CalculationController : ControllerBase
    {
        private readonly ILogger<CalculationController> _logger;
        ICalculationService _calculationService;

        public CalculationController(ILogger<CalculationController> logger, ICalculationService calculationService)
        {
            _logger = logger;
            _calculationService = calculationService;
        }

        [HttpGet]
        public async Task<double> CalculateNetAfterDiscount(double discount, double total)
        {
            var result =  _calculationService.CalculateNetAfterDiscount( discount,  total).Result.Value;
            return result;
        }
        [HttpGet]
        public async Task<double> CalculateNetAfterTax(double tax, double total)
        {
            var result =  _calculationService.CalculateNetAfterTax( tax,  total).Result.Value;
            return result;
        }
    }
}
