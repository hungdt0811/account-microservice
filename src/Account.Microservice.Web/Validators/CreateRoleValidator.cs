using Account.Microservice.Web.ApiModels.Role;
using FluentValidation;
using Newtonsoft.Json;

namespace Account.Microservice.Web.Validators;

public class CreateRoleValidator: AbstractValidator<CreateRoleModel>
{
  public CreateRoleValidator()
  {
    //Valication RollName
    RuleFor(r => r.RoleName).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Tên nhóm quyền không được trống!")
      .NotEmpty().WithMessage("Tên nhóm quyền không được trống!")
      .Length(1, 50).WithMessage("Tên nhóm quyền có độ dài tối đa 50 ký tự!");

    RuleFor(r => r.PermissionIds).Cascade(CascadeMode.Stop)
      .NotNull().WithMessage("Hãy chọn các chức năng cho nhóm quyền!")
      .NotEmpty().WithMessage("Hãy chọn các chức năng cho nhóm quyền!")
      .Must(BeValidJsonArray).WithMessage("Các chức năng được chọn không phù hợp");
  }

  private bool BeValidJsonArray(string permissionIds)
  {
    try
    {
      var permissionIdList = JsonConvert.DeserializeObject<List<int>>(permissionIds);
      if (permissionIdList != null && permissionIdList.Any())
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
