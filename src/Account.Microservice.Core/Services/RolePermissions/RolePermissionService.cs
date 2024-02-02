using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.Entities.SecurityAggregate.Specifications;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Account.Microservice.Core.Services.RolePermissions;
public class RolePermissionService : IRolePermissionService
{
  private readonly IRepository<Permission> _permissionRepository;
  private readonly IRepository<Role> _roleRepository;
  private readonly IRepository<RolePermission> _rolePermissionRepository;

  public RolePermissionService
  (
    IRepository<Permission> permissionRepository,
    IRepository<Role> roleRepository,
    IRepository<RolePermission> rolePermissionRepository,
    IRepository<User> userReposotory
  )
  {
    _permissionRepository = permissionRepository;
    _roleRepository = roleRepository;
    _rolePermissionRepository = rolePermissionRepository;
  }

  #region Create | Update

  /// <summary>
  /// Tạo mới Role
  /// </summary>
  /// <param name="roleName"></param>
  /// <param name="permissionIds"></param>
  /// <param name="isActive"></param>
  /// <param name="note"></param>
  /// <returns>Role || null</returns>
  public async Task<Role?> CreateRoleAsync(int userId, string roleName, string permissionIds, bool isActive, string? note = null)
  {
    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    {
      try
      {
        // Tạo mới một Role
        var role = new Role(roleName, isActive, note);
        role.CreatedBy = userId;
        var result = await _roleRepository.AddAsync(role);

        // Chuyển đổi chuỗi JSON permissionIds thành danh sách các id permission
        var permissionIdList = JsonConvert.DeserializeObject<List<int>>(permissionIds);

        // Kiểm tra danh sách permissionId có rỗng không
        if (permissionIdList != null && permissionIdList.Any())
        {
          // Tạo mới các record RolePermission
          foreach (var permissionId in permissionIdList)
          {
            // Tạo mới một đối tượng RolePermission và thêm vào cơ sở dữ liệu
            var rolePermission = new RolePermission { RoleId = role.Id, PermissionId = permissionId };
            await _rolePermissionRepository.AddAsync(rolePermission);
          }
        }

        scope.Complete();

        return role;
      }
      catch
      {
        return null;
        throw;
      }
    }
  }

  /// <summary>
  /// Cập nhật Role
  /// </summary>
  /// <param name="id"></param>
  /// <param name="roleName"></param>
  /// <param name="permissionIds"></param>
  /// <param name="isActive"></param>
  /// <param name="note"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public async Task<Role?> UpdateRoleAsync(int userId, int id, string roleName, string permissionIds, bool isActive = false, string? note = null)
  {
    var role = await _roleRepository.FirstOrDefaultAsync(new RoleSpecification(id));
    if (role == null)
    {
      return null;
    }

    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    {
      try
      {
        // Cập nhật thông tin của role
        role.RoleName = roleName;
        role.IsActive = isActive;
        role.Note = note;
        role.UpdatedBy = userId;
        role.UpdatedDate = DateTime.UtcNow;

        // Lấy danh sách permissionId cũ của role trong bảng RolePermission
        var existingRolePermissions = await _rolePermissionRepository.ListAsync(new RPSpecification(id));
        var existingPermissionIds = existingRolePermissions.Select(rp => rp.PermissionId).ToList();

        // Chuyển đổi chuỗi JSON permissionIds thành danh sách các id permission
        var permissionIdList = JsonConvert.DeserializeObject<List<int>>(permissionIds);

        // Thêm các permissionId mới vào table RolePermission
        foreach (var permissionId in permissionIdList!)
        {
          if (!existingPermissionIds.Contains(permissionId))
          {
            var rolePermission = new RolePermission { RoleId = role.Id, PermissionId = permissionId };
            await _rolePermissionRepository.AddAsync(rolePermission);
          }
        }

        // Xoá các record RolePermission không có PermissionId trong danh sách permissionIds
        foreach (var existingPermissionId in existingPermissionIds)
        {
          if (!permissionIdList.Contains(existingPermissionId))
          {
            var rolePermissionToDelete = existingRolePermissions.FirstOrDefault(rp => rp.PermissionId == existingPermissionId);
            if (rolePermissionToDelete != null)
            {
              await _rolePermissionRepository.DeleteAsync(rolePermissionToDelete);
            }
          }
        }

        await _roleRepository.UpdateAsync(role);

        scope.Complete();

        return role;
      }
      catch
      {
        return null;
        throw;
      }
    }
  }

  public async Task<bool> DeleteRoleAsync(int id)
  {
    var role = await _roleRepository.GetByIdAsync(id);

    if (role == null)
    {
      return false;
    }

    try
    {
      await _roleRepository.DeleteAsync(role);
      await _roleRepository.SaveChangesAsync();

      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

  #endregion


  #region Get

  /// <summary>
  /// Lấy danh sách permisson có đệ quy theo nhóm cha con
  /// </summary>
  /// <returns>List<Permission>?</returns>
  public async Task<IList<Permission>?> GetAllPermissionAsync()
  {
    var permissions = await _permissionRepository.ListAsync(new PermissionSpecification());

    return permissions;
  }

  public async Task<IList<Role>?> GetAllRoleAsync()
  {
    var roles = await _roleRepository.ListAsync(new RoleSpecification());

    return roles;
  }

  public async Task<Role?> GetDetailRoleAsync(int id)
  {
    var role = await _roleRepository.FirstOrDefaultAsync(new RoleWithRelaPermissionSpecification(id));
    //var role = await _roleRepository.GetByIdAsync(id);

    return role;
  }


  #endregion
}
