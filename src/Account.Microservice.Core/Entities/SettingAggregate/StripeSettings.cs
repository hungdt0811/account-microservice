using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Entities.SettingAggregate;
public class StripeSettings: ISettings
{
  public string SecretKey { get; set; } = string.Empty;
  public string WebhookToken { get; set; } = string.Empty;
  public string Currency { get; set; } = string.Empty;
  public string TaxBehavior { get; set; } = string.Empty;
  public string Recurring { get; set; } = string.Empty;
  public string PaymentMode { get; set; } = string.Empty;
  public string UrlSucess { get; set; } = string.Empty;
  public string CancelUrl { get; set; } = string.Empty;
  public string UrlFailed { get; set; } = string.Empty;
  public string UrlPayment { get; set; } = string.Empty;
  public string PublicKey { get; set; } = string.Empty;
}
