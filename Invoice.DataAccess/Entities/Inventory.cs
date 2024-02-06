using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class Inventory:EntityBase
    {
       
        public long Code { get; set; }
        public long Qty { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("Store")]
        public long StoreId { get; set; }
        public virtual Store Store { get; set; }

        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey("Unit")]
        public long UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
