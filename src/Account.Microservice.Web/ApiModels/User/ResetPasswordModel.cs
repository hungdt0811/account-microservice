namespace Account.Microservice.Web.ApiModels.Users;

public class ResetPasswordModel
{
  public string Key { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string ConfirmPassword { get; set; } = string.Empty;
}
