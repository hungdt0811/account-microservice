using System.Text.RegularExpressions;
using FluentValidation;
using Account.Microservice.Web.Validators;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.ApiModels.Users;

namespace Account.Microservice.Web.Validators;

public class LoginValidator : AbstractValidator<LoginModel>
{
  public LoginValidator()
  {
    //Valication Email
    RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Email không được trống!")
      .NotEmpty().WithMessage("Email không được trống!")
      .Length(5, 100).WithMessage("Email có độ dài tối đa 100 ký tự!")
      .EmailAddress().WithMessage("Email không hợp lệ!");

    //Valication Password
    RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Mật khẩu không được trống!")
      .NotEmpty().WithMessage("Mật khẩu không được trống!")
      .Length(8, 25).WithMessage("Mật khẩu có độ dài từ 8 đến 25 ký tự!")
      .Must(FunctionHelper.ValidatePassword).WithMessage("Mật khẩu cần có chữ thường, chữ in hoa và số!");
  }

}
