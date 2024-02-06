using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataTransferObject.DTOs.Item;

namespace Invoice.Application.Interfaces
{
    public interface IItemService
    {
        Task<Result<List<GetAllItemDTO>>> GetItems();

    }
}
