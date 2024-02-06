using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.Application.Interfaces;

using Invoice.DataAccess.Data;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;

namespace Invoice.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Task<Result<List<GetAllStoresDTO>>> GetStores()
        {
            try
            {
               
               var result=_unitOfWork.StoreRepo.GetAll()
                   .AsEnumerable().Select(x => new GetAllStoresDTO()
                {
                    
                        Id = x.Id,
                        Name = x.Name
                   
                }).ToList()?? new List<GetAllStoresDTO>();

                return Task.FromResult(Result<List<GetAllStoresDTO>>.Successful(result));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<List<GetAllStoresDTO>>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }

       
    }
}
