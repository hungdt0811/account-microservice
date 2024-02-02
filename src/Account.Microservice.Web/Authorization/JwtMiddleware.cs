using Account.Microservice.Core.Services.Users;

namespace Account.Microservice.Web.Authorization;

public class JwtMiddleware
{
  private readonly RequestDelegate _next;

  public JwtMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
  {
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    int? userId = jwtUtils.ValidateToken(token)!;
    if (userId != null)
    {
      // attach user to context on successful jwt validation
      context.Items["User"] = await userService.GetUserByIdAsync(userId.Value);
      context.Items["Permissions"] = await userService.GetPermissonUserByIdAsync(userId.Value);
    }

    await _next(context);
  }
}
