namespace Account.Microservice.Web.ApiModels.Users;

public class AdminSettingModel
{
  public string? UserName { get; set; }
  public string? Password { get; set; }
  public string? Email { get; set; }
  public bool IsChangePassword { get; set; }
}
