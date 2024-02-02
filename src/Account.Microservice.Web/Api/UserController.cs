using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Services.Paginations;
using Account.Microservice.Core.Services.RolePermissions;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Services.Users;
using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.Microservice.Web.Api;

public class UserController : BaseApiController
{
  private readonly IUserService _userService;
  private readonly IUserLocalService _userLocalService;
  private readonly IJwtUtils _jwtUtils;
  private readonly IPaginationService _paginationService;

  public UserController
  (
    IUserService userService,
    IUserLocalService userLocalService,
    IJwtUtils jwtUtils,
    IPaginationService paginationService
  )
  {
    _userService = userService;
    _userLocalService = userLocalService;
    _jwtUtils = jwtUtils;
    _paginationService = paginationService;
  }

  [Authorize]
  [Permission(PermissionConst.UserListStudent)]
  [HttpGet]
  public async Task<IActionResult> GetAllUserAsync()
  {
    var users = await _userService.GetAll();
    return Ok(users);
  }

  [Authorize]
  [HttpGet("profile")]
  public async Task<IActionResult> GetProfileAsync()
  {
    int userId = UserId(_jwtUtils);
    var user = await _userService.GetUserByIdAsync(userId);
    if (user == null)
    {
      return Ok(new BaseResponseModel
      {
        Success = false,
        ErrorMessages = "Không tìm thấy thông tin!"
      });
    }

    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = user
    });
  }

  [Authorize]
  [HttpGet("get-permissions-user")]
  public async Task<IActionResult> GetPermissUserAsync()
  {
    int userId = UserId(_jwtUtils);
    var permissionCodes = await _userService.GetPermissonUserByIdAsync(userId);

    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = permissionCodes
    });
  }

  [Authorize]
  [Permission(PermissionConst.UserAdd)]
  [HttpPost]
  public async Task<IActionResult> CreateUserAsync(CreateUserModel createUserModel)
  {
    int userId = UserId(_jwtUtils);
    var result = await _userLocalService.CreateUserAsync(createUserModel, userId);
    if (!result.Success)
    {
      return Ok( new BaseResponseModel
      {
        Success = false,
        ErrorMessages = result.ErrorMessages
      });
    }

    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = result.Data
    });
  }

  [Authorize]
  [HttpPost]
  [Route("update-profile")]
  public async Task<IActionResult> UpdateUserAsync(UpdateProfileModel model)
  {
    int userId = UserId(_jwtUtils);
    var user = await _userService.GetUserByIdAsync(userId);
    if (user == null)
    {
      return StatusCode(StatusCodes.Status401Unauthorized);
    }

    var result = await _userLocalService.UpdateProfileAsync(model, user);
    if (!result.Success)
    {
      return Ok(new BaseResponseModel
      {
        Success = false,
        ErrorMessages = result.ErrorMessages
      });
    }

    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = result.Data
    });
  }

  [Authorize]
  [HttpPost]
  [Route("update-password")]
  public async Task<IActionResult> UpdatePasswordAsync(ChangePasswordModel model)
  {
    int userId = UserId(_jwtUtils);
    var user = await _userService.GetUserByIdAsync(userId);
    if (user == null)
    {
      return StatusCode(StatusCodes.Status401Unauthorized);
    }

    var result = await _userLocalService.ChangePasswordAsync(model, user);

    if (!result.Success)
    {
      return Ok(new BaseResponseModel
      {
        Success = false,
        ErrorMessages = result.ErrorMessages
      });
    }

    return Ok(new BaseResponseModel
    {
      Success = true
    });
  }

  [Authorize]
  [Permission(PermissionConst.UserListStudent)]
  [HttpGet]
  [Route("get-list-student/studying")]
  public async Task<IActionResult> GetListStudentStudying(int page, int count)
  {
    var students = await _userLocalService.GetListStudentStudyingAsync(page, count);
    if (students is PagedList<UserStudentResponse> pagedList)
    {
      var paginationInfo = _paginationService.GetPaginationInfo(pagedList);
      return Ok(new
      {
        Success = true,
        Data = new
        {
          Users = students,
          PaginationInfo = paginationInfo
        }
      });
    }

    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = students
    });
  }
}
