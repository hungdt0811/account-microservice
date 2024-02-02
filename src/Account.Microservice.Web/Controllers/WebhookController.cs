using Ardalis.Result;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Web.ApiModels.Stripe;
using Account.Microservice.Web.Services.Stripe;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using Stripe.Checkout;

namespace Account.Microservice.Web.Controllers;
public class WebhookController : Controller
{
  private readonly ITripeService _stripeService;
  private readonly StripeSettings _stripeSettings;

  public WebhookController(ITripeService stripeService, StripeSettings stripeSettings)
  {
    _stripeService = stripeService;
    _stripeSettings = stripeSettings;
  }

  public async Task<IActionResult> Index()
  {
    var endpointSecret = _stripeSettings.WebhookToken;
    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    if (string.IsNullOrEmpty(json))
    {
      return Ok();
    }
    try
    {

      var stripeEvent = EventUtility.ConstructEvent(json,
          Request.Headers["Stripe-Signature"], endpointSecret);
      //dynamic objJson = JObject.Parse(json);
      //var payment_intent = objJson!.data["object"].payment_intent;
      if (stripeEvent.Type == Events.CheckoutSessionCompleted)
      {
        var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
        var orderPaid = session?.PaymentStatus == "paid";
       
      }
      
      if (stripeEvent.Type == Events.ChargeRefunded)
      {
        var charge = stripeEvent.Data.Object as Stripe.Charge;
        var chargeStatus = charge?.Status == "succeeded";
        if (chargeStatus)
        {
          //var result = await _orderLocalService.UpdatePaymentStatus(charge!.PaymentIntentId, (int)OrderStatus.Refund, (int)PaymentStatus.Refund);
          //if (!result)
          //{
          //  _logger.LogError($"Error when update status order to refund");
          //}
          //_logger.LogInformation($"Update refund order");
        }
      }
      else
      {
        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
      }

      return Ok();
    }
    catch (StripeException e)
    {
     // _logger.LogError($"Error when listen webhook {e.Message} - {e.StackTrace!}");
      return BadRequest(e.Message);
    }
  }

}
