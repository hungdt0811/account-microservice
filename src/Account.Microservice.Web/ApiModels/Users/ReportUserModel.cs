namespace Account.Microservice.Web.ApiModels.Users;

public class ReportUserModel
{
  public List<string>? Labels { get; set; }
  public List<int>? Data { get; set; }
  public int Total { get; set; }
}
