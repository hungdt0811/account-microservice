namespace Account.Microservice.Web.ViewModels;

public class StripeSettingModel
{
  public string? UrlPayment { get; set; }
  public string? UrlSuccess { get; set; }
  public string? UrlFailure { get; set; }
  public string? PublicKey { get; set; }
}
