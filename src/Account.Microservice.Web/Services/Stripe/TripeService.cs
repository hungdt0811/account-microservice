using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Web.ApiModels.Stripe;
using Stripe;
using Stripe.Checkout;

namespace Account.Microservice.Web.Services.Stripe;

public class TripeService : ITripeService
{
  private readonly ChargeService _chargeService;
  private readonly TokenService _tokenService;
  private readonly StripeSettings _stripeSettings;
  private readonly CommonSettings _commonSettings;
  private readonly ILogger<AddProductStripeModel> _logger;

  public TripeService(
          ChargeService chargeService,
          TokenService tokenService,
          StripeSettings stripeSettings,
          CommonSettings commonSettings,
          ILogger<AddProductStripeModel> logger)
  {
    _chargeService = chargeService;
    _tokenService = tokenService;
    _stripeSettings = stripeSettings;
    StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    _commonSettings = commonSettings;
    _logger = logger;
  }

  public async Task<AddProductStripeResult?> AddProductStripe(AddProductStripeModel addProductStripeModel)
  {
    try
    {
      var options = new ProductCreateOptions
      {
        Name = addProductStripeModel.ProductName,
        //Images = new List<string> { addProductStripeModel.ImageUrl! },
        DefaultPriceData = new ProductDefaultPriceDataOptions
        {
          UnitAmount = addProductStripeModel.Price,
          Currency = _stripeSettings.Currency,
          TaxBehavior = _stripeSettings.TaxBehavior
        },
        Expand = new List<string> { "default_price" },
      };
      var service = new ProductService();
      var result = await service.CreateAsync(options);
      return new AddProductStripeResult
      {
        PriceId = result.DefaultPriceId,
        ProductId = result.Id
      };
    }
    catch(Exception ex)
    {
      _logger.LogError($"Error when add product in stripe {ex.Message}");
      return null;
    }
  }

  public async Task<StripePaymentResult> AddStripePaymentAsync(StripePaymentModel payment, CancellationToken ct)
  {
    StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    // Set Stripe Token options based on customer data
    TokenCreateOptions tokenOptions = new TokenCreateOptions
    {
      Card = new TokenCardOptions
      {
        Name = payment.Name,
        Number = payment.CardNumber,
        ExpYear = payment.ExpirationYear,
        ExpMonth = payment.ExpirationMonth,
        Cvc = payment.Cvc,
      }
    };

    // Create new Stripe Token
    Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);
    // Set the options for the payment we would like to create at Stripe
    ChargeCreateOptions paymentOptions = new ChargeCreateOptions
    {
      Source = stripeToken.Id,
      ReceiptEmail = payment.ReceiptEmail,
      Description = payment.Description,
      Currency = payment.Currency,
      Amount = payment.Amount
    };
    // Create the payment
    var createdPayment = await _chargeService.CreateAsync(paymentOptions, null, ct);

    // Return the payment to requesting method
    return new StripePaymentResult
    {
      Amount = createdPayment.Amount,
      ReceiptEmail = createdPayment.ReceiptEmail,
      Description = createdPayment.Description,
      Currency = createdPayment.Currency,
      PaymentId = createdPayment.Id,
      UrlReceipt = createdPayment.ReceiptUrl,
      Status = createdPayment.Status,
      Paid = createdPayment.Paid
    };
  }

  public async Task<PaymentIntentStatusModel> CheckStatusPayment(string paymentIntent)
  {
    try
    {
      var service = new PaymentIntentService();
      var result = await service.GetAsync(
         paymentIntent
         );
      return new PaymentIntentStatusModel
      {
        Status= result.Status,
        PaymentIntent = paymentIntent
      };
    }
    catch(Exception ex)
    {
      _logger.LogError($"Error when CheckStatusPayment {ex.Message}");
      return new PaymentIntentStatusModel
      {
        PaymentIntent = paymentIntent,
        Status = "fail"
      };
    }
  }

  public async Task<CreateUrlCheckoutResult> CreateUrlCheckout(CreateUrlCheckoutModel createUrlCheckoutModel)
  {
    var domain = !string.IsNullOrEmpty(createUrlCheckoutModel.Domain) ? createUrlCheckoutModel.Domain : _commonSettings.WebsiteUrl;
    var options = new SessionCreateOptions
    {
      LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                   Price = createUrlCheckoutModel.Price,
                   Quantity = createUrlCheckoutModel.Quantity,
                  },
                },
      Mode = _stripeSettings.PaymentMode,
      SuccessUrl = $"{domain}/{_stripeSettings.UrlSucess}",
      CancelUrl = $"{domain}/{_stripeSettings.CancelUrl}",
      AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
      CustomerEmail = createUrlCheckoutModel.CustomerEmail,
    };
    var service = new SessionService();
    Session session = await service.CreateAsync(options);
    return new CreateUrlCheckoutResult { Url = session.Url, Id = session.Id };
  }

  public async Task<UpdateProductStripeReult?> UpdateProductStripe(UpdateProductStripeModel updateProductStripeModel)
  {
    try
    {
      var options = new PriceCreateOptions
      {
        Product = updateProductStripeModel.ProductId,
        UnitAmount = updateProductStripeModel.Price,
        Currency = _stripeSettings.Currency,
        //Recurring = new PriceRecurringOptions { Interval = _stripeSettings.Recurring },
        Active = true,//set price to default
        TaxBehavior = _stripeSettings.TaxBehavior
      };
      var service = new PriceService();
      var result = await service.CreateAsync(options);
      return new UpdateProductStripeReult { PriceId = result.Id };
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error when update price in stripe {ex.Message}");
      return null;
    }
    
  }
}
