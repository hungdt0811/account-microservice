using Account.Microservice.Core.Constants;

namespace Account.Microservice.Web.ApiModels.Users;

public class UpdateProfileModel
{
  public UpdateProfileModel(string fullName)
  {
    FullName = fullName;
  }

  public string FullName { get; set; }
  public int? Sex { get; set; }
  public string? Address { get; set; }
  public string? Mobile { get; set; }
  public string? Birthday { get; set; }
  public string? Avatar { get; set; }

}
