using System.Text.RegularExpressions;
using Account.Microservice.Web.ApiModels;
using FluentValidation;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.Validators;
using Account.Microservice.Web.ApiModels.Users;

public class AdminSettingValidator : AbstractValidator<AdminSettingModel>
{
  public AdminSettingValidator()
  {
    RuleFor(p => p.UserName).NotNull().NotEmpty().Length(3,160);
    RuleFor(p => p.Password).NotNull().NotEmpty().Length(8, 25).Must(FunctionHelper.ValidatePassword).When(p=>p.IsChangePassword);
    RuleFor(p => p.Email).NotNull().NotEmpty().Length(5, 120).EmailAddress();
  }
}
