using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.SecurityAggregate;

namespace Account.Microservice.Core.Services.RolePermissions;
public interface IRolePermissionService
{
  /// <summary>
  /// Lấy danh sách permisson có đệ quy theo nhóm cha con
  /// </summary>
  /// <returns>List<Permission>?</returns>
  Task<IList<Permission>?> GetAllPermissionAsync();

  Task<IList<Role>?> GetAllRoleAsync();

  Task<Role?> GetDetailRoleAsync(int id);

  /// <summary>
  /// Tạo mới Role
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="roleName"></param>
  /// <param name="permissionIds"></param>
  /// <param name="isActive"></param>
  /// <param name="note"></param>
  /// <returns>Role || null</returns>
  Task<Role?> CreateRoleAsync(int userId, string roleName, string permissionIds, bool isActive = false, string? note = null);

  /// <summary>
  /// Cập nhật Role
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="id"></param>
  /// <param name="roleName"></param>
  /// <param name="permissionIds"></param>
  /// <param name="isActive"></param>
  /// <param name="note"></param>
  /// <returns>Role || null</returns>
  Task<Role?> UpdateRoleAsync(int userId, int id, string roleName, string permissionIds, bool isActive = false, string? note = null);

  Task<bool> DeleteRoleAsync(int id);
}
