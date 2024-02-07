using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.DataAccess.Entities;
using Invoice.DataTransferObject.DTOs.Invoice;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [HelperClasses.Authorize]
    public class InvoiceOrderController : ControllerBase
    {
        private readonly ILogger<InvoiceOrderController> _logger;
        IInvoiceService _invoiceService;

        public InvoiceOrderController(ILogger<InvoiceOrderController> logger, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<Result<GetInvoiceDTO>> SaveInvoice([FromBody]SaveInvoiceDTO invoiceDto)
        {
            var user = (User)HttpContext.Items["User"]!;
            var result = await _invoiceService.SaveInvoice(invoiceDto, user.Id.ToString());
            return result;
        }

        [HttpGet]
        public async Task<Result<List<GetInvoiceDTO>>> GetInvoices()
        {
            var result = await _invoiceService.GetInvoices();
            return result;
        }
        [HttpGet]
        public async Task<GetInvoiceDTO> GetInvoiceById(long id)
        {
            var result =  _invoiceService.GetInvoiceById(id).Result.Value;
            return result;
        }
    }
}
