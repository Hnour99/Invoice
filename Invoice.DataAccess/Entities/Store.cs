using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class Store: EntityBase
    {
        public Store()
        {
            Items = new HashSet<Item>();
            Inventories = new HashSet<Inventory>();
            Invoices = new HashSet<Invoice>();
        }
        public virtual ICollection<Item> Items { get; set; }
       
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

      
        public long Code { get; set; }
        public string? Name { get; set; }
    }
}
