using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.DataAccess.Entities;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;

namespace Invoice.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<Result<User>> Login(string userName, string password);
        Task<Result<User>> GetUser(string userName);
        Task<Result<User>> GetUserById(string UserId);
    }
}
