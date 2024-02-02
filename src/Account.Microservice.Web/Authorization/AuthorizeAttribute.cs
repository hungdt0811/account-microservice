
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;

namespace Account.Microservice.Web.Authorization;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
  public void OnAuthorization(AuthorizationFilterContext context)
  {
    try
    {
      // skip authorization if action is decorated with [AllowAnonymous] attribute
      var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
      if (allowAnonymous)
        return;

      // authorization
      var user = context.HttpContext.Items["User"] as User;
      var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
      if (token != null && token.StartsWith("Bearer "))
      {
        token = token.Substring("Bearer ".Length).Trim();
      }
      var handler = new JwtSecurityTokenHandler();
      var jsonToken = handler.ReadToken(token);
      var tokenS = handler.ReadToken(token) as JwtSecurityToken;

      var email = "";
      if (tokenS != null)
      {
        var findEemail = tokenS.Claims.FirstOrDefault(x => x.Type == "email");
        if (findEemail != null)
        {
          email = findEemail.Value;
        }
      }

      if (user == null || user.Status != (int)UserStatus.Active || string.IsNullOrEmpty(email) || user.Email != email)
        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    } catch
    {
      context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
  }
}
