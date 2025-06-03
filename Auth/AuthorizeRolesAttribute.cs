using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Huellitas.Clases;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    private readonly string[] allowedRoles;

    public AuthorizeRolesAttribute(params string[] roles)
    {
        allowedRoles = roles;
    }

    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            actionContext.Response = actionContext.ControllerContext.Request
                .CreateResponse(HttpStatusCode.Unauthorized, "No autorizado");
        }
        else
        {
            actionContext.Response = actionContext.ControllerContext.Request
                .CreateResponse(HttpStatusCode.Forbidden, "No tiene permisos suficientes");
        }
    }

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var request = actionContext.Request;
        string token = null;

        if (request.Headers.Contains("X-Auth-Token"))
        {
            var headerValue = request.Headers.GetValues("X-Auth-Token").FirstOrDefault();
            if (!string.IsNullOrEmpty(headerValue) && headerValue.StartsWith("Bearer "))
            {
                token = headerValue.Substring("Bearer ".Length).Trim();
            }
        }
        else if (HttpContext.Current.Request.Cookies["auth_token"] != null)
        {
            token = HttpContext.Current.Request.Cookies["auth_token"].Value;
        }
        else if (request.Headers.Authorization?.Scheme == "Bearer")
        {
            token = request.Headers.Authorization.Parameter;
        }

        if (string.IsNullOrEmpty(token))
            return false;

        var principal = TokenManager.ValidateToken(token);
        if (principal == null || !principal.Identity.IsAuthenticated)
            return false;

        HttpContext.Current.User = principal;
        actionContext.RequestContext.Principal = principal;

        return allowedRoles.Length == 0 ||
               allowedRoles.Any(role =>
                   principal.Claims.Any(c =>
                       c.Type == ClaimTypes.Role &&
                       string.Equals(c.Value, role, StringComparison.OrdinalIgnoreCase)));
    }
}
