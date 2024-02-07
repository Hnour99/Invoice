using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.Application.Interfaces;
using Invoice.DataAccess.Data;
using Invoice.DataAccess.Entities;
using Invoice.DataTransferObject.DTOs.Invoice;
using Invoice.DataTransferObject.DTOs.Item;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Invoice.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Task<Result<GetInvoiceDTO>> GetInvoiceById(long id)
        {
            try
            {
                var result = _unitOfWork.InvoiceRepo.GetAll().Where(x => x.Id == id)

                    .Include(c => c.Store).Include(c => c.InvoiceDetailss)
                    .ThenInclude(x => x.Item)
                    .Select(x => new GetInvoiceDTO()
                    {
                        Id = x.Id,
                        StoreName = x.Store.Name,
                        SerialNo = x.SerialNo.ToString(),
                        Total = Convert.ToInt64(x.Total).ToString(),
                        Tax = Convert.ToInt64(x.Tax).ToString(),
                        Net = Convert.ToInt64(x.Net).ToString(),
                        InvoiceDate = x.InvoiceDate.ToShortDateString(),
                        InvoiceDetailss = x.InvoiceDetailss.AsEnumerable().Select(c => new GetInvoiceDetailsDTO()
                        {
                            Net = Convert.ToInt64(c.Net).ToString(),
                            Total = Convert.ToInt64(c.Total).ToString(),
                            Discount = Convert.ToInt64(c.Discount).ToString(),
                            Price = Convert.ToInt64(c.Price).ToString(),
                            Qty = Convert.ToInt64(c.Qty).ToString(),
                            ItemName = c.Item.Name
                        }).ToList()


                    }).FirstOrDefault() ?? new GetInvoiceDTO();

                return Task.FromResult(Result<GetInvoiceDTO>.Successful(result));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<GetInvoiceDTO>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }

        public Task<Result<List<GetInvoiceDTO>>> GetInvoices()
        {
            try
            {

                var result = _unitOfWork.InvoiceRepo.GetAll().Include(c => c.Store)
                    .AsEnumerable().Select(x => new GetInvoiceDTO()
                    {
                        Id = x.Id,
                        StoreName = x.Store.Name,
                        SerialNo = x.SerialNo.ToString(),
                        Total = x.Total.ToString(),
                        Tax = x.Tax.ToString(),
                        Net = x.Net.ToString(),
                        InvoiceDate = x.InvoiceDate.ToShortDateString()


                    }).ToList() ?? new List<GetInvoiceDTO>();

                return Task.FromResult(Result<List<GetInvoiceDTO>>.Successful(result));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<List<GetInvoiceDTO>>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }

        public Task<Result<GetInvoiceDTO>> SaveInvoice(SaveInvoiceDTO invoiceDto, string userId)
        {
            var invoiceDetailsLis = invoiceDto.InvoiceDetailss.AsEnumerable().Select(e => new InvoiceDetails()
            {
                ItemId = e.ItemId,
                UnitId = e.UnitId,
                Price = e.Price,
                Qty = e.Qty,
                Discount = e.Discount,
                Total = e.Total,
                Net = e.Net,
                StoreId = e.StoreId
            }).ToList();
            var invoice = new DataAccess.Entities.Invoice()
            {
                StoreId = invoiceDto.StoreId,
                SerialNo = invoiceDto.SerialNo,
                Total = invoiceDto.Total,
                Tax = invoiceDto.Tax,
                Net = invoiceDto.Net,
                InvoiceDate = invoiceDto.InvoiceDate,
                InvoiceDetailss = invoiceDetailsLis
            };
            _unitOfWork.InvoiceRepo.Add(invoice);
            var re = _unitOfWork.SaveChanges(userId);
            var result = new GetInvoiceDTO();
            return Task.FromResult(Result<GetInvoiceDTO>.Successful(result));
        }
    }
}
