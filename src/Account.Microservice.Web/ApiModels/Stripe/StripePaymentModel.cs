namespace Account.Microservice.Web.ApiModels.Stripe;

public class StripePaymentModel
{
  public string ReceiptEmail { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string Currency { get; set; } = string.Empty;
  public long Amount { get; set; }
  //information card
  public string Name { get; set; } = string.Empty;
  public string CardNumber { get; set; } = string.Empty;
  public string ExpirationYear { get; set; } = string.Empty;
  public string ExpirationMonth { get; set; } = string.Empty;
  public string Cvc { get; set; } = string.Empty;
}
