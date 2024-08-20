using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OauthExample.MyWebApi.Services;

namespace OauthExample.MyWebApi.Config
{
    public class TokenValidationFilter : IActionFilter
    {
        private readonly IAuthenticationService _authService;
        public TokenValidationFilter(IAuthenticationService authService) 
        {
            _authService = authService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                if (!_authService.IsAccessTokenValid(token))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
