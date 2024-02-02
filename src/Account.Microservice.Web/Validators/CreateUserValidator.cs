using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Helpers;
using Ardalis.Result;
using FluentValidation;
using Newtonsoft.Json;

namespace Account.Microservice.Web.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
  private readonly IUserService _userService;
  public CreateUserValidator(IUserService userService)
  {
    _userService = userService;

    //Valication FullName
    RuleFor(p => p.FullName).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Họ và tên không được trống!")
      .NotEmpty().WithMessage("Họ và tên không được trống!")
      .Length(1, 50).WithMessage("Họ và tên có độ dài tối đa 50 ký tự!");

    //Valication Email
    RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Email không được trống!")
      .NotEmpty().WithMessage("Email không được trống!")
      .Length(5, 100).WithMessage("Email có độ dài tối đa 100 ký tự!")
      .EmailAddress().WithMessage("Email không hợp lệ!")
      .Must(IsEmailUnique).WithMessage("Email đã tồn tại!");

    //Valication Password
    RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Mật khẩu không được trống!")
      .NotEmpty().WithMessage("Mật khẩu không được trống!")
      .Length(8, 25).WithMessage("Mật khẩu có độ dài từ 8 đến 25 ký tự!")
      .Must(FunctionHelper.ValidatePassword).WithMessage("Mật khẩu cần có chữ thường, chữ in hoa và số!");

    RuleFor(p => p.Status).Cascade(CascadeMode.Stop)
      .Must(status => Enum.IsDefined(typeof(UserStatus), status))
      .WithMessage("Trạng thái tài khoản không phù hợp!");

    RuleFor(p => p.Type).Cascade(CascadeMode.Stop)
      .Must(types => Enum.IsDefined(typeof(UserType), types))
      .WithMessage("Kiểu tài khoản không phù hợp!");

    RuleFor(p => p.RoleIds).Cascade(CascadeMode.Stop)
      .Must(BeValidJsonArray).WithMessage("Nhóm quyền không phù hợp!");
  }

  private bool IsEmailUnique(string email)
  {
    var user = _userService.GetUserByEmail(email);
    return user == null;
  }

  private bool BeValidJsonArray(string? roleIds)
  {
    try
    {
      if (roleIds == null)
        return true;

      var roleIdList = JsonConvert.DeserializeObject<List<int>>(roleIds);
      if (roleIdList != null && roleIdList.Any())
      {
        return true;
      }
      return false;
    }
    catch (Exception)
    {
      return false;
    }
  }
}
