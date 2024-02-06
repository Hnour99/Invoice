using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.Invoice
{
    public class InvoiceItemDTO
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public int ItemTotal { get; set; }
        public string ItemDiscount { get; set; }
        public double ItemNet { get; set; }
    }
}
