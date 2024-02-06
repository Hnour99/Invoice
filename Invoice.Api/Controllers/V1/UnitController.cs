using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.DTOs.Unit;
using Invoice.DataTransferObject.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [HelperClasses.Authorize]
    public class UnitController : ControllerBase
    {
        private readonly ILogger<UnitController> _logger;
        IUnitService _unitServic;

        public UnitController(ILogger<UnitController> logger, IUnitService unitService)
        {
            _logger = logger;
            _unitServic = unitService;
        }
        [HttpGet]
        public async Task<Result<List<GetAllUnitDTO>>> GetUnits()
        {
            var result =await _unitServic.GetUnits();
            return result;
        }
    }
}
