using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Invoice.DataTransferObject.DTOs.LogIn;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Invoice.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly RestClient _client;
        private readonly ILogger<InvoiceOrderController> _logger;
        private readonly IConfiguration _configuration;
        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            var x = _configuration.GetSection("InvoiceApiV1").Value;
            _client = new RestClient($"{_configuration.GetSection("InvoiceApiV1").Value}/api/v1/"); ;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LogInDTO model)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            var request = new RestRequest($"TokenAuth/TokenLogIn?userName={model.UserName}&password={model.Password}");

            var response = await _client.ExecuteGetAsync(request);
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                var token = JsonSerializer.Deserialize<string>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
               
                HttpContext.Session.SetString("token", token);
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "UserId").Value;

                var role = jwtSecurityToken.Claims.First(claim => claim.Type == "UserRole").Value;


                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name,  userId),
                    new Claim(ClaimTypes.Role, role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;


                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "/"
            };
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Login");
        }
        [HttpGet]
        public  ViewResult AccessDenied()
        {
            return View();
        }
    }
}
