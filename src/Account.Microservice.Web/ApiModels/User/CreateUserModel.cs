using Account.Microservice.Core.Constants;

namespace Account.Microservice.Web.ApiModels.Users;

public class CreateUserModel
{
  public CreateUserModel
  (
    string fullName,
    string email,
    string password,
    bool isSystemRole = false,
    string? roleIds = null,
    int status = (int)UserStatus.Deactive,
    int type = (int)UserType.Student
  )
  {
    FullName = fullName;
    Email = email;
    Password = password;
    RoleIds = roleIds;
    IsSystemRole = isSystemRole;
    Status = status;
    Type = type;
  }
  public string FullName { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public string? RoleIds { get; set; } = null;
  public bool IsSystemRole { get; set; } = false;
  public int Status { get; set; } = (int)UserStatus.Deactive;
  public int Type { get; set; } = (int)UserType.Student;
}
