using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class Item:EntityBase
    {
       
        public Item()
        {
            Inventories = new HashSet<Inventory>();
            InvoiceDetailss = new HashSet<InvoiceDetails>();
        }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetailss { get; set; }
        public long Code { get; set; }
        public string? Name { get; set; }

        [ForeignKey("Store")]
        public long StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
