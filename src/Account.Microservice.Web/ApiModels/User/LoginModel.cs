namespace Account.Microservice.Web.ApiModels.Users;

public class LoginModel
{
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public bool RememberMe { get; set; } = false;
  public string Token { get; set; } = string.Empty;
}
