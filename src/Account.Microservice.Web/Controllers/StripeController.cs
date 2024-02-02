using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Account.Microservice.Web.Controllers;
public class StripeController : Controller
{
  private readonly StripeSettings _stripeSettings;

  public StripeController(StripeSettings stripeSettings)
  {
    _stripeSettings = stripeSettings;
  }
  public IActionResult Index()
  {
    var model = new StripeSettingModel
    {
      UrlFailure = _stripeSettings.UrlFailed,
      UrlPayment = _stripeSettings.UrlPayment,
      UrlSuccess = _stripeSettings.UrlSucess,
      PublicKey= _stripeSettings.PublicKey,
    };
    return View(model);
  }
}
