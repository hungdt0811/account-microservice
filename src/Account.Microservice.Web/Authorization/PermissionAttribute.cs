
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
public class PermissionAttribute : Attribute, IAuthorizationFilter
{
  public string PermissionCode { get; }

  public PermissionAttribute(string permissionCode)
  {
    PermissionCode = permissionCode;
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    var permissions = context.HttpContext.Items["Permissions"] as IList<string>;
    if (permissions == null || !permissions.Contains(PermissionCode))
    {
      context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
    }
  }
}
