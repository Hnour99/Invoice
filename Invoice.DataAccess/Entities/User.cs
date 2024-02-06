using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Entities
{
    public class User:EntityBase
    {
       
        public long Code { get;  set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Role")]
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
