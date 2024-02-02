namespace Account.Microservice.Web.ApiModels.Stripe;

public class CreateUrlCheckoutResult
{
  public string Url { get; set; } = string.Empty;
  public string Id { get; set; } = string.Empty;
}
