using System.Net;
using Invoice.Api.HelperClasses;
using Invoice.Application.Interfaces;
using Invoice.DataAccess.Entities;
using Invoice.DataTransferObject.DTOs.LogIn;
using Invoice.DataTransferObject.DTOs.Store;
using Invoice.DataTransferObject.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Invoice.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenAuthController : ControllerBase
    {
        private readonly ILogger<TokenAuthController> _logger;
        private readonly ISecurityService _securityService;

        private readonly AppSettings _appSettings;
        public TokenAuthController(ILogger<TokenAuthController> logger, ISecurityService securityService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _securityService = securityService;
            _appSettings= appSettings.Value;

        }

        [HttpGet]
        public async Task<IActionResult> TokenLogIn(string userName,string password)
        {
            var checkUser = _securityService.Login(userName, password);
            if(checkUser.Result.Type==ResultType.Failed)
            {
                return Unauthorized();
            }
            else
            {
                var token = TokenManager.Login(checkUser.Result.Value, _appSettings.Secret);
                return Ok(token);
                
            }
            
            
        }
        
        
    }
}
