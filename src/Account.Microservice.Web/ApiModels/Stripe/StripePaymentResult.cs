namespace Account.Microservice.Web.ApiModels.Stripe;

public class StripePaymentResult
{
  public string ReceiptEmail { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string Currency { get; set; } = string.Empty;
  public long Amount { get; set; }
  public string PaymentId { get; set; } = string.Empty;
  public string UrlReceipt { get; set; } = string.Empty;
  public string Status { get; set; }=string.Empty;
  public bool? Paid { get; set; } 
}
