using Account.Microservice.Web.ApiModels.Users;
using FluentValidation;

namespace Account.Microservice.Web.Validators;

public class RequestResetPasswordValidator : AbstractValidator<RequestResetPasswordModel>
{
  public RequestResetPasswordValidator()
  {
    RuleFor(r => r.Email).NotNull().NotEmpty().Length(5, 120).EmailAddress();
  }
}
