namespace Account.Microservice.Web.ApiModels.Stripe;

public class PaymentIntentStatusModel
{
  public string? Status { get; set; }
  public string? PaymentIntent { get; set; }
}
