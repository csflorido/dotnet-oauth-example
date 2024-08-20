using Microsoft.AspNetCore.Mvc;
using OauthExample.MyWebApi.Exceptions;
using OauthExample.MyWebApi.Models;
using OauthExample.MyWebApi.Services;

namespace OauthExample.MyWebApi.Controllers
{

    [ApiController]
    [Route("oauth")]
    public class OAuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public OAuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public IActionResult GetBearerToken([FromBody]TokenRequest request)
        {
            try
            {
                var tokenResult = _authService.RequestAccessToken(request);
                return Ok(tokenResult);
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.ProblemDetails);
            }
            catch(UnauthorizedException)
            {
                return Unauthorized();
            }
        }
    }
}
