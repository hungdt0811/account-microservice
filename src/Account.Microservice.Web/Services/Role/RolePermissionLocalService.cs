using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Services.RolePermissions;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Role;

namespace Account.Microservice.Web.Services.Role;

public class RolePermissionLocalService : IRolePermissionLocalService
{
  private readonly IRolePermissionService _rolePermissionService;

  public RolePermissionLocalService(IRolePermissionService rolePermissionService)
  {
    _rolePermissionService = rolePermissionService;
  }

  /// <summary>
  /// Tạo mới Role
  /// </summary>
  /// <param name="createRoleModel"></param>
  /// <returns>BaseResponseModel</returns>
  public async Task<BaseResponseModel> CreateRoleAsync(CreateRoleModel createRoleModel, int userId)
  {
    var role = await _rolePermissionService.CreateRoleAsync(userId, createRoleModel.RoleName, createRoleModel.PermissionIds, createRoleModel.IsActive, createRoleModel.Note);
    if (role == null)
    {
      return ResponseModel.RespondFailure(ErrorCode.SystemError, "Lỗi tạo nhóm quyền!");
    }

    return ResponseModel.RespondSuccess(role);
  }

  /// <summary>
  /// Cập nhật Role
  /// </summary>
  /// <param name="createRoleModel"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public async Task<BaseResponseModel> UpdateRoleAsync(CreateRoleModel createRoleModel, int id, int userId)
  {
    var role = await _rolePermissionService.UpdateRoleAsync(userId, id, createRoleModel.RoleName, createRoleModel.PermissionIds, createRoleModel.IsActive, createRoleModel.Note);
    if (role == null)
    {
      return ResponseModel.RespondFailure(ErrorCode.SystemError, "Lỗi cập nhật nhóm quyền!");
    }

    return ResponseModel.RespondSuccess(role);
  }
}
