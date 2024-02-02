using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Services.RolePermissions;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Role;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Services.Role;
using Microsoft.AspNetCore.Mvc;

namespace Account.Microservice.Web.Api;

public class RoleController : BaseApiController
{
  private readonly IUserService _userService;
  private readonly IRolePermissionService _rolePermissionService;
  private readonly IRolePermissionLocalService _rolePermissionLocalService;
  private readonly IJwtUtils _jwtUtils;

  public RoleController
  (
    IUserService userService,
    IRolePermissionService rolePermissionService,
    IRolePermissionLocalService rolePermissionLocalService,
    IJwtUtils jwtUtils
  )
  {
    _userService = userService;
    _rolePermissionService = rolePermissionService;
    _rolePermissionLocalService = rolePermissionLocalService;
    _jwtUtils = jwtUtils;
  }

  [Authorize]
  [Permission(PermissionConst.RoleList)]
  [HttpGet]
  public async Task<IActionResult> GetAllRoleAsync()
  {
    var roles = await _rolePermissionService.GetAllRoleAsync();
    return Ok(roles);
  }

  [Authorize]
  [Permission(PermissionConst.RoleEdit)]
  [HttpGet("{id}")]
  public async Task<IActionResult> GetDetailRoleAsync(int id)
  {
    var role = await _rolePermissionService.GetDetailRoleAsync(id);
    if (role == null)
    {
      return Ok(new BaseResponseModel
      {
        Success = false,
        ErrorMessages = "Không tìm thấy thông tin nhóm quyền!"
      });
    }
    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = role
    });
  }

  [Authorize]
  [HttpGet("pemissions")]
  public async Task<IActionResult> GetAllPermissionAsync()
  {
    var permissions = await _rolePermissionService.GetAllPermissionAsync();
    return Ok(permissions);
  }

  [Authorize]
  [Permission(PermissionConst.RoleAdd)]
  [HttpPost]
  public async Task<IActionResult> CreateRoleAsync(CreateRoleModel createRoleModel)
  {
    int userId = UserId(_jwtUtils);
    var result = await _rolePermissionLocalService.CreateRoleAsync(createRoleModel, userId);
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
  [Permission(PermissionConst.RoleEdit)]
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateRoleAsync(CreateRoleModel createRoleModel, int id)
  {
    int userId = UserId(_jwtUtils);
    var result = await _rolePermissionLocalService.UpdateRoleAsync(createRoleModel, id, userId);
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
  [Permission(PermissionConst.RoleDelete)]
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteRoleAsync(int id)
  {
    var result = await _rolePermissionService.DeleteRoleAsync(id);
    return Ok(new BaseResponseModel
    {
      Success = result,
    });
  }
}
