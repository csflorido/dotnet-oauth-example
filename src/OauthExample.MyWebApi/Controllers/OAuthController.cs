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

        /// <summary>
        /// Get Bearer Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
