namespace Account.Microservice.Web.ApiModels.Users;

public class CreateEmailRelatedModel
{

  public int UserId { get; set; }
  public string Profile { get; set; } = string.Empty;
  public int MediaId { get; set; }  
  public List<EmailRelatedItem>? EmailRelatedItems { get; set; }
  //public IFormFile? file { get; set; }
}
