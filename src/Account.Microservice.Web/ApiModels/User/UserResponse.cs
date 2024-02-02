using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Web.ViewModels;

namespace Account.Microservice.Web.ApiModels.Users;

public class UserResponse
{
  public int Id { get; set; }
  public string? Email { get; set; }
  public string? FullName { get; set; }
  public string? Avatar { get; set; }
  public UserResponse ToModel(User user)
  {
    return new UserResponse
    {
      Id = user.Id,
      Email = user.Email,
      FullName = user.FullName,
      Avatar = user.Avatar,
    };
  }
}

