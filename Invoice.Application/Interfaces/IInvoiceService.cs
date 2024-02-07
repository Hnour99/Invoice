using Invoice.DataTransferObject.DTOs.Unit;
using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataTransferObject.DTOs.Invoice;

namespace Invoice.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<Result<GetInvoiceDTO>> SaveInvoice(SaveInvoiceDTO invoiceDto, string userId);
        Task<Result<List<GetInvoiceDTO>>> GetInvoices();
        Task<Result<GetInvoiceDTO>> GetInvoiceById(long id);
    }
}
