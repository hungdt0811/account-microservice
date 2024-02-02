namespace Account.Microservice.Web.ApiModels.Users;

public class EmailRelatedModel
{
  public EmailRelatedModel()
  {
    EmailRelated = new List<string>();
  }
  public string Email { get; set; } = string.Empty;
  public string CompanyName { get; set; } = string.Empty;
  public string Address { get; set; } = string.Empty;
  public string Profile { get; set; } = string.Empty;
  public string EmailSecondary { get; set; } = string.Empty;
  public string MediaUrl { get; set; } = string.Empty;
  public int MediaId { get; set; } = 0;
  public List<string> EmailRelated { get; set; }
}
