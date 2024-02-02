using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Web.Authorization;

public interface IJwtUtils
{
  public string GenerateToken(User user);
  public int? ValidateToken(string? token);
}
