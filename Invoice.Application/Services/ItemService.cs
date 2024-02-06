using Invoice.Application.Interfaces;
using Invoice.DataAccess.Data;
using Invoice.DataTransferObject.DTOs.Item;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Task<Result<List<GetAllItemDTO>>> GetItems()
        {
            try
            {

                var result = _unitOfWork.ItemRepo.GetAll()
                    .AsEnumerable().Select(x => new GetAllItemDTO()
                    {

                        Id = x.Id,
                        Name = x.Name

                    }).ToList() ?? new List<GetAllItemDTO>();

                return Task.FromResult(Result<List<GetAllItemDTO>>.Successful(result));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<List<GetAllItemDTO>>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }
    }
}
