namespace Account.Microservice.Web.ApiModels.Stripe;

public class UpdateProductStripeModel
{
  public string? ProductId { get; set; }
  public long Price { get; set; }

}
