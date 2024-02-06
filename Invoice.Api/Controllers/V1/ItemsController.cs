using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.DataTransferObject.DTOs.Item;
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
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        IItemService _itemService;

        public ItemsController(ILogger<ItemsController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }
        [HttpGet]
        public async Task<Result<List<GetAllItemDTO>>> GetItems()
        {
            var result = await _itemService.GetItems();
            return result;
        }
    }
}
