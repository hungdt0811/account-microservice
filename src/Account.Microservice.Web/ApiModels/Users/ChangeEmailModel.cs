namespace Account.Microservice.Web.ApiModels.Users;

public class ChangeEmailModel
{
  public int UserId { get; set; }
  public string? Email { get; set; }
}
