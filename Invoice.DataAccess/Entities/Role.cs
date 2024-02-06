using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class Role:EntityBase
    {
        public long Code { get;  set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
        public virtual ICollection<User> Users { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
