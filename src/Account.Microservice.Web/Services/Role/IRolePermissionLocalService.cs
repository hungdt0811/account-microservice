using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Role;

namespace Account.Microservice.Web.Services.Role;

public interface IRolePermissionLocalService
{
  /// <summary>
  /// Tạo mới Role
  /// </summary>
  /// <param name="createRoleModel"></param>
  /// <returns>BaseResponseModel</returns>
  Task<BaseResponseModel> CreateRoleAsync(CreateRoleModel createRoleModel, int userId);

  /// <summary>
  /// Cập nhật Role
  /// </summary>
  /// <param name="createRoleModel"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<BaseResponseModel> UpdateRoleAsync(CreateRoleModel createRoleModel, int id, int userId);
}
