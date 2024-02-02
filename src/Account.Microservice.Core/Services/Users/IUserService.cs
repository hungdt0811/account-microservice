using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Helpers;
using Account.Microservice.Core.Interfaces;
using Account.Microservice.Core.Services.Paginations;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.Users;
public interface IUserService
{
  #region CUD

  Task<User?> CreateUserAsync(int userCreatedId, string fullName, string email, string password, bool isSystemRole, string? roleIds, int status, int type);

  Task<User?> UpdateProfileAsync(User user, string fullName, string? address, string? birthday, string? avatar, int? Sex, string? mobile);

  Task DeleteUserAsync(User user);

  #endregion

  #region Get

  Task<IReadOnlyList<User>> GetList(string userName, int? active);
  Task<IPagedList<User>> GetListPaging(int pageIndex, int itemsPage, int roleId, string email, string sortBy, bool ascending, int? status);
  Task<IList<User>> GetAll();
  Task<IReadOnlyList<User>> GetListSearchAutoComplete(string userName, int branchId, int companyId);

  /// <summary>
  /// Get user
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<User?> GetUserByIdAsync(int id);

  /// <summary>
  /// Get User by email include Branch and Company
  /// </summary>
  /// <param name="email"></param>
  /// <returns></returns>
  Task<User?> GetUserByEmailAsync(string email);
  Task<User?> GetUserByEmailSecondaryAsync(string email);

  Task<User?> GetUserByTokenAsync(string email, string token);

  /// <summary>
  /// Get user by phone number
  /// </summary>
  /// <param name="phone"></param>
  /// <returns></returns>
  Task<User?> GetUserByPhoneAsync(string phone);


  Task<User?> GetByKeyForgetPassword(string email);


  /// <summary>
  /// 
  /// </summary>
  /// <param name="pageIndex"></param>
  /// <param name="pageSize"></param>
  /// <param name="userName"></param>
  /// <param name="companyName"></param>
  /// <param name="branchName"></param>
  /// <param name="isActive"></param>
  /// <param name="roleId"></param>
  /// <returns></returns>
  Task<IPagedList<User>> GetUsersAsync(int pageIndex, int pageSize, string userName, string companyName, string branchName, int? isActive, int? roleId);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="code"></param>
  /// <returns></returns>
  Task<User?> GetUserByCodeAsync(string code);

  Task<User?> GetUserBySellerId(int sellerId);

  Task<User?> GetUserBySecretCodeAsync(string code);

  #endregion

  #region Check Role User
  Task<bool> CheckRoleAsync(int userId, string permissionCode);
  #endregion

  #region Update user information

  Task UpdateAsync(User user);

  Task UpdateUserCodeAsync(int userId);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <returns></returns>
  Task UpdateUserAuthentication(User user);
  Task<User> GetUserIncludeBranchCompanyRoleByEmailAsync(string email);

  Task<List<User>> GetAll(IList<int> lstUserId);

  Task<IPagedList<User>> GetListSeller(int pageIndex, int itemsPage, string searchKey, int? status, string sortBy, bool ascending);

  Task<int> CountAsync();
  Task<int> CountAsync(int roleId);
  List<ReportUser> CountGroupStatus(int role);
  #endregion

  Task<List<User>> GetAllAdminActive();

  User? GetUserByEmail(string email);

  Task<BaseResponse> LoginUserCms(string email, string password);

  Task<bool> ChangePasswordAsync(string password, User user);

  Task<IList<string?>?> GetPermissonUserByIdAsync(int userId);

  Task<IPagedList<User>> GetListStudentStudyingAsync(int page, int count);
}
