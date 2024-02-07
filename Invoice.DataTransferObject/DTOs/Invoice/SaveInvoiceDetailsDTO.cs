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
        [Required (ErrorMessage = "Qty is required")]
        public long Qty { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        
        public double Net { get; set; }
        
        public double Total { get; set; }
        [Required]
        public double Discount { get; set; }

        
        public long StoreId { get; set; }
        [Required(ErrorMessage = "Item is required")]
        public long ItemId { get; set; }
        [Required(ErrorMessage = "Unit is required")]
        public long UnitId { get; set; }


        

    }
}
