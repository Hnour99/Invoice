using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.Item
{
    public class GetAllItemDTO
    {
        public long Id { get; set; }
        public long Code { get; set; }
        public string? Name { get; set; }
    }
}
