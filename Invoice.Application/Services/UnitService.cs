using Invoice.DataAccess.Data;
using Invoice.Application.Interfaces;
using Invoice.DataTransferObject.DTOs.Unit;
using Invoice.DataTransferObject.Generic;

namespace Invoice.Application.Services
{
    public class UnitService:IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public Task<Result<List<GetAllUnitDTO>>> GetUnits()
        {
            try
            {
                var result=_unitOfWork.UnitRepo.GetAll()
                    .AsEnumerable().Select(x => new GetAllUnitDTO()
                {

                    Id = x.Id,
                    Name = x.Name

                }).ToList() ?? new List<GetAllUnitDTO>();
                return Task.FromResult(Result<List<GetAllUnitDTO>>.Successful(result));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<List<GetAllUnitDTO>>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }
    }
}
