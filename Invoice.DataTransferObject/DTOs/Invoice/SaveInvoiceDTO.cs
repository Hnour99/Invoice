using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.Invoice
{
    public class SaveInvoiceDTO
    {
        public SaveInvoiceDTO()
        {
            InvoiceDetailss = new List<SaveInvoiceDetailsDTO>();
        }

        public virtual ICollection<SaveInvoiceDetailsDTO> InvoiceDetailss { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        public long SerialNo { get; set; }
        [Required(ErrorMessage = "Net")]
        public double Net { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public long StoreId { get; set; }
        

    }
}
