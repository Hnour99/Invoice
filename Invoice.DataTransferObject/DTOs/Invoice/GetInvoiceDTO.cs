using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.Invoice
{
    public class GetInvoiceDTO
    {
        public long Id { get; set; }
        public string SerialNo { get; set; }
        public string Net { get; set; }
        public string Total { get; set; }
        public string Tax { get; set; }
        public string InvoiceDate { get; set; }
        public string StoreName { get; set; }
       
    }
}
