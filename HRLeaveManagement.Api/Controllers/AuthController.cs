using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationServices;

        public AuthController(IAuthService authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var response = await _authenticationServices.Login(request);

            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegistrationRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var response = await _authenticationServices.Register(request);

            return Ok(response);
        }
    }
}
