using Account.Microservice.Web.ApiModels.Stripe;

namespace Account.Microservice.Web.Services.Stripe;

public interface ITripeService
{
  Task<StripePaymentResult> AddStripePaymentAsync(StripePaymentModel payment, CancellationToken ct);
  Task<AddProductStripeResult?> AddProductStripe(AddProductStripeModel addProductStripeModel);
  Task<UpdateProductStripeReult?> UpdateProductStripe(UpdateProductStripeModel addProductStripeModel);
  Task<CreateUrlCheckoutResult> CreateUrlCheckout(CreateUrlCheckoutModel createUrlCheckoutModel);
  Task<PaymentIntentStatusModel> CheckStatusPayment(string paymentIntent);
}
