namespace Account.Microservice.Web.ApiModels.Stripe;

public class AddProductStripeModel
{
  public string? ProductName { get; set; }
  public long Price { get; set; }
  public string? ImageUrl { get; set; }
}
