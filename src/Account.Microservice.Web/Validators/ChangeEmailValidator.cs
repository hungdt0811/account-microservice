using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Helpers;
using FluentValidation;

namespace Account.Microservice.Web.Validators;

public class ChangeEmailValidator : AbstractValidator<ChangeEmailModel>
{
  public ChangeEmailValidator()
  {
    RuleFor(p => p.Email).NotNull().NotEmpty().Length(5, 120).EmailAddress();
  }
}
