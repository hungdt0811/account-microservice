using System.Text.RegularExpressions;
using FluentValidation;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.Validators;
using Account.Microservice.Web.ApiModels.Users;

public class ResetPasswordValidator: AbstractValidator<ResetPasswordModel>
{
  public ResetPasswordValidator()
  {
    RuleFor(p => p.Key).NotNull().NotEmpty();
    RuleFor(p => p.Password).NotNull().NotEmpty().Length(8, 25).Must(FunctionHelper.ValidatePassword);
    RuleFor(p => p.ConfirmPassword).NotNull().NotEmpty().Length(8, 25);
    RuleFor(p=>p.ConfirmPassword).Equal(p=>p.Password).WithMessage("Passwords must match"); ;
  }
}
