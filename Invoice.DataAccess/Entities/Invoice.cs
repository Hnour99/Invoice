using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class Invoice:EntityBase
    {
        public Invoice()
        {
            InvoiceDetailss = new HashSet<InvoiceDetails>();
        }

        public virtual ICollection<InvoiceDetails> InvoiceDetailss { get; set; }
        public long SerialNo { get; set; }
        public double Net { get; set; }
        public double Total { get; set; }
        public double Tax { get; set; }
        public DateTime InvoiceDate { get; set; }
        [ForeignKey("Store")]
        public long StoreId { get; set; }
        public virtual Store Store { get; set; }

    }
}
