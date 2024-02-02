namespace Account.Microservice.Web.ApiModels.Users;

public class UpdatePasswordModel
{
  public int UserId { get; set; }
  public string? Password { get; set; }
}
