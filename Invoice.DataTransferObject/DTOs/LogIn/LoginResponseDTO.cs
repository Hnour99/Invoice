using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataTransferObject.DTOs.LogIn
{
    public class LoginResponseDTO
    {
        public LoginResponseDTO()
        {
            ExpiresAt = DateTime.Now.AddMinutes(30);

        }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public string AccessToken { get; set; }
        public long? UserRoleId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
