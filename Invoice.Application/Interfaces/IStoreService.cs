using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;

namespace Invoice.Application.Interfaces
{
    public interface IStoreService
    {
        Task<Result<List<GetAllStoresDTO>>> GetStores();

    }
}
