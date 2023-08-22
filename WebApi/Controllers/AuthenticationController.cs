using Application.Authentication.Commands.Authenticate;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.RequestJwtToken;
using Application.Authentication.Commands.RevokeRefreshToken;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> Register(RegisterCommand request)
        {
            var response = await _sender.Send(request);
            return StatusCode(response.Status, response);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateCommand request)
        {
            var response = await _sender.Send(request);
            return StatusCode(response.Status, response);
        }

        [HttpPost("requestJwtToken")]
        public async Task<IActionResult> RequestJwtToken(RequestJwtTokenCommand request)
        {
            var response = await _sender.Send(request);
            return StatusCode(response.Status, response);
        }

        [HttpPost("revokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken(RevokeRefreshTokenCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

    }
}
