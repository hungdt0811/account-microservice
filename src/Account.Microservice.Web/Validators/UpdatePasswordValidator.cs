using System.Text.RegularExpressions;
using Account.Microservice.Web.ApiModels;
using FluentValidation;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.Validators;
using Account.Microservice.Web.ApiModels.Users;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordModel>
{
  public UpdatePasswordValidator()
  {
    RuleFor(p => p.Password).NotNull().NotEmpty().Length(8, 25).Must(FunctionHelper.ValidatePassword);
  }
}
