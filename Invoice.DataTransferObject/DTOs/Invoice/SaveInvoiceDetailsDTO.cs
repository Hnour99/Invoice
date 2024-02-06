using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.Invoice
{
    public class SaveInvoiceDetailsDTO
    {
        [Required]
        public long Qty { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Net { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public double Discount { get; set; }

        [Required]
        public long StoreId { get; set; }
        [Required]
        public long ItemId { get; set; }
        [Required]
        public long UnitId { get; set; }


        

    }
}
