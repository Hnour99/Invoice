using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataTransferObject.DTOs.Unit;

namespace Invoice.Application.Interfaces
{
    public interface IUnitService
    {
        Task<Result<List<GetAllUnitDTO>>> GetUnits();
    }
}
