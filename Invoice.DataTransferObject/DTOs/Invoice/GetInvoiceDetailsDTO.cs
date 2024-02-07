using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.Invoice
{
    public class GetInvoiceDetailsDTO
    {
        public string Price { get; set; }

        public string Net { get; set; }

        public string Total { get; set; }
        
        public string Discount { get; set; }

        public string Qty { get; set; }

        public string StoreName { get; set; }
       
        public string ItemName { get; set; }
       
        public string UnitName { get; set; }

    }
}
