using System.Web.Http;
using System.Web.Http.Controllers;
using RodesAPI.Infra;

namespace RodesAPI.Services
{
    public class AuthAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return TokenService.ValidateToken(actionContext.Request.Headers.Get("token"));
        }

    }
}