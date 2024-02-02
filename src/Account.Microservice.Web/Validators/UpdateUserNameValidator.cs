using System.Text.RegularExpressions;
using Account.Microservice.Web.ApiModels;
using FluentValidation;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.Validators;
using Account.Microservice.Web.ApiModels.Users;

public class UpdateUserNameValidator : AbstractValidator<UpdateUserNameModel>
{
  public UpdateUserNameValidator()
  {
    RuleFor(p => p.UserName).NotNull().NotEmpty().Length(3,250);
  }
}
