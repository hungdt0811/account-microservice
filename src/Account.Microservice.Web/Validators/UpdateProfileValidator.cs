using System.Globalization;
using System.Text.RegularExpressions;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Helpers;
using Ardalis.Result;
using FluentValidation;
using Newtonsoft.Json;

namespace Account.Microservice.Web.Validators;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileModel>
{
  public UpdateProfileValidator()
  {
    //Valication FullName
    RuleFor(p => p.FullName).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Họ và tên không được trống!")
      .NotEmpty().WithMessage("Họ và tên không được trống!")
      .Length(1, 50).WithMessage("Họ và tên có độ dài tối đa 50 ký tự!");

    // Validation Sex
    RuleFor(p => p.Sex).Must(genders => genders != null && Enum.IsDefined(typeof(UserGender), genders))
      .When(p => p.Sex != null)
      .WithMessage("Giới tính không hợp lệ!");

    // Validation Address
    RuleFor(p => p.Address)
      .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.Address))
      .WithMessage("Địa chỉ có độ dài tối đa 500 ký tự!");

    // Validation Mobile
    RuleFor(p => p.Mobile)
      .MaximumLength(20).When(p => !string.IsNullOrEmpty(p.Mobile))
      .WithMessage("Số điện thoại không hợp lệ!");

    // Validation Birthday
    RuleFor(p => p.Birthday).Must(BeValidDate).WithMessage("Ngày sinh không hợp lệ! (dd-mm-yyy)");

    // Validation Avatar
    RuleFor(p => p.Avatar)
      .MaximumLength(255).When(p => !string.IsNullOrEmpty(p.Avatar))
      .WithMessage("Đường dẫn ảnh đại diện có độ dài tối đa 255 ký tự!");
  }

  private bool BeValidDate(string? date)
  {
    if (string.IsNullOrEmpty(date))
      return true;

    if (!Regex.IsMatch(date, @"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$"))
      return false;

    return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
  }
}
