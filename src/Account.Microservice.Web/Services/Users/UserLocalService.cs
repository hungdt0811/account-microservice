using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Entities.UserAggregate.Specifications;
using Account.Microservice.Core.Helpers;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Extensions;

namespace Account.Microservice.Web.Services.Users;

public class UserLocalService : IUserLocalService
{
  private readonly IUserService _userService;
  private readonly IJwtUtils _jwtUtils;

  public UserLocalService
  (
    IUserService userService,
    IJwtUtils jwtUtils
  )
  {
    _userService = userService;
    _jwtUtils = jwtUtils;
  }

  public async Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel model, User user)
  {
    if (!PasswordHelper.VerifyPassword(model.OldPassword, user.Password))
    {
      return ResponseModel.RespondFailure(ErrorCode.NoData, "Mật khẩu cũ không chính xác!");
    }

    bool result = await _userService.ChangePasswordAsync(model.Password, user);
    if (result)
    {
      return ResponseModel.RespondSuccess();
    }
    return ResponseModel.RespondFailure(ErrorCode.SystemError, "Lỗi đổi mật khẩu!");
  }

  public async Task<BaseResponseModel> CreateUserAsync(CreateUserModel createUserModel, int userCreatedId)
  {
    var user = await _userService.CreateUserAsync(
      userCreatedId,
      createUserModel.FullName,
      createUserModel.Email,
      createUserModel.Password,
      createUserModel.IsSystemRole,
      createUserModel.RoleIds,
      createUserModel.Status,
      createUserModel.Type
      );
    if (user == null)
    {
      return ResponseModel.RespondFailure(ErrorCode.SystemError, "Lỗi tạo tài khoản!");
    }
    return ResponseModel.RespondSuccess(user);
  }

  public async Task<IPagedList<UserStudentResponse>> GetListStudentStudyingAsync(int page, int count)
  {
    var pagedListUser = await _userService.GetListStudentStudyingAsync(page, count);
    var pagedListUserResponse = pagedListUser.ToPagedListResponse();
    return pagedListUserResponse;
  }

  public async Task<BaseResponseModel> LoginUserCms(LoginModel loginModel)
  {
    var result = await _userService.LoginUserCms(loginModel.Email, loginModel.Password);
    if (!result.Success)
    {
      return ResponseModel.RespondFailure(ErrorCode.NoData, result.Messages);
    }
    string token = _jwtUtils.GenerateToken(result.Data);
    return new BaseResponseModel
    {
      Success = true,
      Token = token,
      Data = UserExtensions.ToResponseModel(result.Data)
    };
  }

  public async Task<BaseResponseModel> UpdateProfileAsync(UpdateProfileModel model, User user)
  {
    var userUpdated = await _userService.UpdateProfileAsync(user, model.FullName, model.Address, model.Birthday, model.Avatar, model.Sex, model.Mobile);
    if (userUpdated == null)
    {
      return ResponseModel.RespondFailure(ErrorCode.SystemError, "Lỗi cập nhật thông tin tài khoản!");
    }
    return new BaseResponseModel { Success = true, Data = userUpdated};
  }
}
