using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Web.ApiModels.Stripe;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Account.Microservice.Web.Api;
public class StripeController : BaseApiController
{
  private readonly StripeSettings _stripeSettings;

  public StripeController(StripeSettings stripeSettings)
  {
    _stripeSettings = stripeSettings;
    StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
  }

  [HttpPost("create-payment-intent")]
  public ActionResult Create(PaymentIntentCreateRequest paymentIntentCreateRequest)
  {
    if (paymentIntentCreateRequest.Amount == null) return BadRequest();
    var paymentIntentService = new PaymentIntentService();
    var paymentIntent =  paymentIntentService.Create(new PaymentIntentCreateOptions
    {
      Amount = paymentIntentCreateRequest.Amount,
      Currency = _stripeSettings.Currency,
      //AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
      //{
      //  Enabled = true,
      //}
      PaymentMethodTypes = new List<string>
      {
        "card",
      }
    });

    return Json(new { clientSecret = paymentIntent.ClientSecret });
  }
}
