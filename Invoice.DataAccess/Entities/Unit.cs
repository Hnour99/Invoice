using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class Unit:EntityBase
    {
       
        public long Code { get;  set; }
        public Unit()
        {
            Inventories = new HashSet<Inventory>();
            InvoiceDetailss = new HashSet<InvoiceDetails>();
        }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetailss { get; set; }
        public string? Name { get; set; }
    }
}
