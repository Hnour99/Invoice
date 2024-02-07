using Invoice.DataTransferObject.DTOs.Invoice;
using Invoice.DataTransferObject.DTOs.Store;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Text.Json;
using Invoice.DataTransferObject.DTOs.Item;
using Invoice.DataTransferObject.DTOs.Unit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Invoice.DataTransferObject.Generic;

namespace Invoice.UI.Controllers
{
    public class InvoiceOrderController : Controller
    {
        private readonly ILogger<InvoiceOrderController> _logger;
        private readonly RestClient _client;
        public InvoiceOrderController(ILogger<InvoiceOrderController> logger)
        {
            _logger = logger;
            _client = new RestClient("https://localhost:7149/api/v1/");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ActionFilteToken))]
        public async Task<IActionResult> Index()
        {
            
            var invoicesList = await GetAllInvoices();
            return View(invoicesList);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ActionFilteToken))]
        public async Task<IActionResult> Create()
        {
           
            ViewBag.Stores = await GetAllStores();
            ViewBag.Units = await GetAllUnits();
            ViewBag.Items = await GetAllItems();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ActionFilteToken))]
        public async Task<IActionResult> InvoiceDetails(long id)
        {
            var invoice= GetInvoiceById(id).Result;
            return View(invoice);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ActionFilteToken))]
        public async Task<IActionResult> Save(string serialNo
            , string invoiceDate, string storeId
            , string totalAmount, string tax, string net, string itemList)
        {
            if (ModelState.IsValid)
            {
                var invoiceItemLiit = JsonSerializer.Deserialize<List<InvoiceItemDTO>>(itemList);
                var invoiceDetailsListDto = invoiceItemLiit.AsEnumerable().Select(e => new SaveInvoiceDetailsDTO()
                {
                    StoreId = Convert.ToInt64(storeId),
                    ItemId = Convert.ToInt64(e.ItemId),
                    UnitId = Convert.ToInt64(e.UnitId),
                    Price = Convert.ToDouble(e.Price),
                    Qty = Convert.ToInt64(e.Qty),
                    Total = Convert.ToDouble(e.ItemTotal),
                    Discount = Convert.ToDouble(e.ItemDiscount),
                    Net = Convert.ToDouble(e.ItemNet)
                }).ToList();
                var invoiceDto = new SaveInvoiceDTO();
                invoiceDto.StoreId = Convert.ToInt64(storeId);
                invoiceDto.SerialNo = Convert.ToInt64(serialNo);
                invoiceDto.InvoiceDate = DateTime.Now;
                invoiceDto.Total = Convert.ToDouble(totalAmount);
                invoiceDto.Tax = Convert.ToDouble(tax);
                invoiceDto.Net = Convert.ToDouble(net);
                invoiceDto.InvoiceDetailss = invoiceDetailsListDto;

                
                var request = new RestRequest("InvoiceOrder/SaveInvoice");
                var token = HttpContext.Session.GetString("token");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                var json = JsonSerializer.Serialize(invoiceDto);
                request.AddJsonBody(json);
                var response = await _client.PostAsync(request);
                var getInvoiceDTO = new GetInvoiceDTO();
                if (response.IsSuccessful)
                {
                    

                    return  Json(1);
                }
                return Json(0);
            }

          
            
            return  Json("0");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ActionFilteToken))]
        public async Task<IActionResult> CalculateNetAfterDiscount(string discount, string total)
        {
            var request = new RestRequest($"Calculation/CalculateNetAfterDiscount?discount={Convert.ToDouble(discount)}&total={Convert.ToDouble(total)}");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
           
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);
            
            if (response.IsSuccessful)
            {
                total = JsonSerializer.Deserialize<double> (response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ToString();
            }

            return Json(total);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ActionFilteToken))]
        public async Task<IActionResult> CalculateNetAfterTax(string tax, string total)
        {
            var request = new RestRequest($"Calculation/CalculateNetAfterTax?tax={Convert.ToDouble(tax)}&total={Convert.ToDouble(total)}");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-Length", "0");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);

            if (response.IsSuccessful)
            {
                total = JsonSerializer.Deserialize<double>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ToString();
            }

            return Json(total);
        }

        private async Task<List<GetInvoiceDTO>> GetAllInvoices()
        {
            var request = new RestRequest("InvoiceOrder/GetInvoices");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-Length", "0");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);
            var stores = new List<GetInvoiceDTO>();
            if (response.IsSuccessful)
            {
                stores = JsonSerializer.Deserialize<Root<GetInvoiceDTO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).value.ToList();
            }

            return stores;
        }

        private async Task<GetInvoiceDTO> GetInvoiceById(long id)
        {
            var request = new RestRequest($"InvoiceOrder/GetInvoiceById?id={id}");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-Length", "0");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);
            var invoice = new GetInvoiceDTO();
            if (response.IsSuccessful)
            {
                invoice = JsonSerializer.Deserialize<GetInvoiceDTO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return invoice;
        }

        private async Task<List<GetAllStoresDTO>> GetAllStores()
        {
            var request = new RestRequest("Store/GetStores");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-Length", "0");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);
            var stores = new List<GetAllStoresDTO>();
            if (response.IsSuccessful)
            {
                stores = JsonSerializer.Deserialize<Root<GetAllStoresDTO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).value.ToList();
            }

            return stores;
        }
        private async Task<List<GetAllItemDTO>> GetAllItems()
        {
            var request = new RestRequest("Items/GetItems");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-Length", "0");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);
            var stores = new List<GetAllItemDTO>();
            if (response.IsSuccessful)
            {
                stores = JsonSerializer.Deserialize<Root<GetAllItemDTO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).value.ToList();
            }

            return stores;
        }
        private async Task<List<GetAllUnitDTO>> GetAllUnits()
        {
            var request = new RestRequest("Unit/GetUnits");
            var token = HttpContext.Session.GetString("token");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-Length", "0");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await _client.ExecuteGetAsync(request);
            var stores = new List<GetAllUnitDTO>();
            if (response.IsSuccessful)
            {
                stores = JsonSerializer.Deserialize<Root<GetAllUnitDTO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).value.ToList();
            }

            return stores;
        }
        
        public class Root<T>
        {
            public List<T> value { get; set; }
            public int type { get; set; }
            public object errors { get; set; }
        }

    }
}
