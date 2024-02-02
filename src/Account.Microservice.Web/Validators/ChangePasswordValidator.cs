using System.Text.RegularExpressions;
using FluentValidation;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.Validators;
using Account.Microservice.Web.ApiModels.Users;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
{
  public ChangePasswordValidator()
  {
    RuleFor(p => p.OldPassword).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Mật khẩu cũ không được trống!")
      .NotEmpty().WithMessage("Mật khẩu cũ không được trống!");

    RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Mật khẩu mới không được trống!")
      .NotEmpty().WithMessage("Mật khẩu mới không được trống!")
      .Length(8, 25).WithMessage("Mật khẩu có độ dài từ 8 đến 25 ký tự!")
      .Must(FunctionHelper.ValidatePassword).WithMessage("Mật khẩu cần có chữ thường, chữ in hoa và số!");

    RuleFor(p => p.ConfirmPassword).Equal(p => p.Password).WithMessage("Mật khẩu nhập lại không chính xác!");
  }
}
