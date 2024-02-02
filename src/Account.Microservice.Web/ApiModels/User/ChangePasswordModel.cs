namespace Account.Microservice.Web.ApiModels.Users;

public class ChangePasswordModel
{
  public string OldPassword { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string ConfirmPassword { get; set; } = string.Empty;
}
