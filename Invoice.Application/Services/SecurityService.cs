using Invoice.Application.Interfaces;
using Invoice.DataAccess.Data;
using Invoice.DataAccess.Entities;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Application.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Task<Result<User>> GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User>> GetUserById(string userId)
        {
            try
            {
                var result = _unitOfWork.UserRepo
                    .GetAll().Single(e => e.Id == Convert.ToInt64(userId) && e.IsActive);
                if (result.UserName != null)
                {
                    return Task.FromResult(Result<User>.Successful(result));
                }
                return Task.FromResult(Result<User>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<User>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }

        public Task<Result<User>> Login(string userName,string password)
        {
            
            try
            {
                var result = _unitOfWork.UserRepo.GetAll().Include(e=>e.Role)
                        .Where(e => e.UserName == userName && e.Password == password&&e.IsActive).Single();
                if (result.UserName != null)
                {
                    return Task.FromResult(Result<User>.Successful(result));
                }
                return Task.FromResult(Result<User>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
            catch (Exception)
            {
                return Task.FromResult(Result<User>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        


        }
    }
}
