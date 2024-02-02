namespace Account.Microservice.Web.ApiModels.Stripe;

public class CreateUrlCheckoutModel
{
  public long Quantity { get; set; }
  public string? Price { get; set; }
  public string? CustomerEmail { get; set; }
  public string? Domain { get; set;}
}
