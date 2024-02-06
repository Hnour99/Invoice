using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Invoice.Api.Controllers.V1;
using Invoice.Application.Interfaces;
using Invoice.DataAccess.Entities;
using Invoice.DataTransferObject.DTOs.LogIn;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Invoice.Api.HelperClasses
{
    public static class TokenManager
    {
        public static string Login(User user,string se)
        {
            //var RoleOfUser = _securityService.;
            //LoginResponseDTO loginResponse = new LoginResponseDTO()
            //{
            //    UserId = result.Id,
            //    FullName = result.Person.FullNameAr,
            //    SSN = result.Person.SSN,
            //    UserRoleId = RoleOfUser
            //};
            var tokenHandeler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(se);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("FullName", user.FullName),
                    new Claim("UserRole", user.Role.Name)
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandeler.CreateToken(tokenDesc);

            
            return tokenHandeler.WriteToken(token);
        }
    }
}
