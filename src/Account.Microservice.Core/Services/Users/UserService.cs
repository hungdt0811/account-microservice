using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Entities.UserAggregate.Specifications;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.Core.Helpers;
using System.Data;
using Newtonsoft.Json;
using System.Transactions;
using Account.Microservice.Core.Entities.SecurityAggregate.Specifications;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Account.Microservice.Core.Constants;

namespace Account.Microservice.Core.Services.Users;
public class UserService : IUserService
{
  #region Fields

  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Role> _roleRepository;
  private readonly IRepository<Permission> _permissionRepository;
  private readonly IRepository<RolePermission> _rolePermissionRepository;
  private readonly IRepository<UserRole> _userRoleRepository;
  private readonly ICodeGeneratorService _codeGeneratorService;

  #endregion

  #region Ctor
  public UserService
  (
    IRepository<User> userRepository,
    IRepository<Role> roleRepository,
    ICodeGeneratorService codeGeneratorService,
    IRepository<UserRole> userRoleRepository,
    IRepository<RolePermission> rolePermissionRepository,
    IRepository<Permission> permissionRepository
  )
  {
    _userRepository = userRepository;
    _roleRepository = roleRepository;
    _codeGeneratorService = codeGeneratorService;
    _userRoleRepository = userRoleRepository;
    _rolePermissionRepository = rolePermissionRepository;
    _permissionRepository = permissionRepository;
  }
  #endregion

  #region CUD

  /// <summary>
  /// Admin tạo User mới
  /// </summary>
  /// <param name="userCreatedId"></param>
  /// <param name="fullName"></param>
  /// <param name="email"></param>
  /// <param name="password"></param>
  /// <param name="isSystemRole"></param>
  /// <param name="roleIds"></param>
  /// <param name="status"></param>
  /// <param name="type"></param>
  /// <returns>User || null</returns>
  public async Task<User?> CreateUserAsync(int userCreatedId, string fullName, string email, string password, bool isSystemRole, string? roleIds, int status, int type)
  {
    password = PasswordHelper.HashPassword(password);
    var codeConfirm = PasswordHelper.GeneratePassword(true, true, true, false, 30);

    var user = new User(fullName, email, password);
    user.CodeConfirm = codeConfirm;
    user.IsSystemRole = isSystemRole;
    user.Status = status;
    user.Type = type;
    user.CreatedBy = userCreatedId;

    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    {
      try
      {
        var addedUser = await _userRepository.AddAsync(user);
        if (addedUser == null)
          throw new Exception("Failed to create user.");

        if (!string.IsNullOrEmpty(roleIds))
        {
          var roleIdList = JsonConvert.DeserializeObject<List<int>>(roleIds);
          if (roleIdList != null && roleIdList.Any())
          {
            foreach (var roleId in roleIdList)
            {
              // Kiểm tra Role có tồn tại hay không
              var role = await _roleRepository.GetByIdAsync(roleId);
              if (role != null)
              {
                // Nếu Role tồn tại thì thêm mới record vào table UserRole
                var userRole = new UserRole { UserId = addedUser.Id, RoleId = roleId };
                await _userRoleRepository.AddAsync(userRole);
              }
            }
          }
        }

        scope.Complete();
        return addedUser;
      }
      catch (Exception)
      {
        return null;
        throw;
      }
    }
  }

  public async Task<User?> UpdateProfileAsync(User user, string fullName, string? address, string? birthday, string? avatar, int? sex, string? mobile)
  {
    user.FullName = fullName;
    user.Address = address;
    user.Avatar = avatar;
    user.Sex = sex;
    user.Mobile = mobile;

    try
    {
      if (!string.IsNullOrEmpty(birthday))
      {
        if (DateTime.TryParseExact(birthday, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedBirthday))
        {
          user.Birthday = parsedBirthday;
        }
        else
        {
          throw new ArgumentException("Ngày sinh không hợp lệ");
        }
      }

      await _userRepository.UpdateAsync(user);

      return user;
    } catch
    {
      return null;
      throw;
    }
  }
  #endregion

  #region Get

  /// <summary>
  /// Get User by email is active
  /// </summary>
  /// <param name="email"></param>
  /// <returns></returns>
  public async Task<User?> GetUserByTokenAsync(string email, string token)
  {
    return await _userRepository.FirstOrDefaultAsync(new UsersSpecification(email, token));
  }

  /// <summary>
  /// Get user by phone number
  /// </summary>
  /// <param name="phone"></param>
  /// <returns></returns>
  public async Task<User?> GetUserByPhoneAsync(string phone)
  {
    var userSpec = new UserPhoneSpecification(phone);
    return await _userRepository.FirstOrDefaultAsync(userSpec);
  }


  /// <summary>
  /// Get user include Role
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public async Task<User?> GetUserByIdAsync(int id)
  {
    return await _userRepository.GetByIdAsync(id);
  }

  public async Task UpdateAsync(User user)
  {
    await _userRepository.UpdateAsync(user);
  }

  public async Task DeleteUserAsync(User user)
  {
    await _userRepository.DeleteAsync(user);
  }

  public Task<IPagedList<User>> GetUsersAsync(int pageIndex, int pageSize, string userName, string companyName, string branchName, int? isActive, int? roleId)
  {
    throw new NotImplementedException();
  }

  public async Task<IList<User>> GetAll()
  {
    var filter = new UsersSpecification();
    return await _userRepository.ListAsync(filter);
  }

  #endregion

  #region Check user role
  public async Task<bool> CheckRoleAsync(int userId, string permissionCode)
  {
    var user = await _userRepository.FirstOrDefaultAsync(new UserIncludeRoleById(userId));
    if (user == null)
      return false;

    if (user.IsSystemRole)
      return true;

    var roles = user.Roles;
    if (roles == null || !roles.Any())
      return false;

    var roleIds = roles.Select(r => r.Id).ToList();

    var roleWithParmissions = await _roleRepository.FirstOrDefaultAsync(new RoleWithRelaPermissionSpecification(roleIds));

    if (roleWithParmissions != null)
    {
      var listPermissionCode = roleWithParmissions.Permissions?.Select(p => p.Code).ToList();
      if (listPermissionCode != null && listPermissionCode.Contains(permissionCode))
      {
        return true;
      }
    }

    return false;
  }

  public async Task<IList<string?>?> GetPermissonUserByIdAsync(int userId)
  {
    var user = await _userRepository.FirstOrDefaultAsync(new UserIncludeRoleById(userId));
    if (user == null)
      return null;

    if (user.IsSystemRole)
    {
      var allPermission = await _permissionRepository.ListAsync();
      var permissionCodes = allPermission.Select(p => p.Code).ToList();
      return permissionCodes;
    }

    var roles = user.Roles;
    if (roles == null || !roles.Any())
      return null;

    var roleIds = roles.Select(r => r.Id).ToList();

    var roleWithParmissions = await _roleRepository.FirstOrDefaultAsync(new RoleWithRelaPermissionSpecification(roleIds));

    if (roleWithParmissions != null)
    {
      var listPermissionCode = roleWithParmissions.Permissions?.Select(p => p.Code).ToList();
      return listPermissionCode;
    }

    return null;
  }
  #endregion

  #region Update user information

  public Task UpdateUserCodeAsync(int userId)
  {
    throw new NotImplementedException();
  }

  public Task<IList<User>> GetHeadOfficeUsersByBranchId(int[] branchIds)
  {
    throw new NotImplementedException();
  }


  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <returns></returns>
  public Task UpdateUserAuthentication(User user)
  {
    throw new NotImplementedException();
  }

  Task<IPagedList<User>> IUserService.GetUsersAsync(int pageIndex, int pageSize, string userName, string companyName, string branchName, int? isActive, int? roleId)
  {
    throw new NotImplementedException();
  }

  public Task<User> GetUserIncludeBranchCompanyRoleByEmailAsync(string email)
  {
    throw new NotImplementedException();
  }

  public Task<IList<Role>> GetAllRolesAsync(bool showHidden = false)
  {
    throw new NotImplementedException();
  }

  public async Task<List<User>> GetAll(IList<int> lstUserId)
  {
    var specification = new UsersSpecification(lstUserId);
    return await _userRepository.ListAsync(specification);
  }

  public async Task<List<User>> GetAllAdminActive()
  {
    var specification = new UsersSpecification(adminActive: true);
    return await _userRepository.ListAsync(specification);
  }

  public async Task<int> CountAsync()
  {
    var specification = new UsersSpecification();
    var count = await _userRepository.CountAsync(specification);
    return count;
  }

  public async Task<int> CountAsync(int roleId)
  {
    var specification = new UsersByRoleSpecification(roleId);
    var count = await _userRepository.CountAsync(specification);
    return count;
  }
  public List<ReportUser> CountGroupStatus(int role)
  {
    throw new NotImplementedException();
  }

  public async Task<User?> GetUserBySecretCodeAsync(string code)
  {
    var specification = new UsersBySecretCodeSpecification(code);
    return await _userRepository.FirstOrDefaultAsync(specification);
  }

  public Task<IReadOnlyList<User>> GetList(string userName, int? active)
  {
    throw new NotImplementedException();
  }

  public Task<IPagedList<User>> GetListPaging(int pageIndex, int itemsPage, int roleId, string email, string sortBy, bool ascending, int? status)
  {
    throw new NotImplementedException();
  }

  public Task<IReadOnlyList<User>> GetListSearchAutoComplete(string userName, int branchId, int companyId)
  {
    throw new NotImplementedException();
  }

  public Task<User?> GetUserByEmailAsync(string email)
  {
    throw new NotImplementedException();
  }

  public Task<User?> GetUserByEmailSecondaryAsync(string email)
  {
    throw new NotImplementedException();
  }

  public Task<User?> GetByKeyForgetPassword(string email)
  {
    throw new NotImplementedException();
  }

  public Task<User?> GetUserByCodeAsync(string code)
  {
    throw new NotImplementedException();
  }

  public Task<User?> GetUserBySellerId(int sellerId)
  {
    throw new NotImplementedException();
  }

  public Task<IPagedList<User>> GetListSeller(int pageIndex, int itemsPage, string searchKey, int? status, string sortBy, bool ascending)
  {
    throw new NotImplementedException();
  }

  public User? GetUserByEmail(string email)
  {
    var query = from b in _userRepository.Table
                where b.Email == email
                select b;
    var user = query.FirstOrDefault();
    return user;
  }
  #endregion

  /// <summary>
  /// Login User cho CMS
  /// </summary>
  /// <param name="email"></param>
  /// <param name="password"></param>
  /// <returns>User || null</returns>
  public async Task<BaseResponse> LoginUserCms(string email, string password)
  {
    try
    {
      var user = await _userRepository.FirstOrDefaultAsync(new UsersSpecification(email));
      if (user == null)
        return DataResponse.RespondFailure("Tài khoản không chính xác!");

      if (user.Type == (int)UserType.Student && !user.IsSystemRole)
        return DataResponse.RespondFailure("Tài khoản không chính xác!");

      if (user.Status == (int)UserStatus.Deactive)
        return DataResponse.RespondFailure("Tài khoản chưa kích hoạt!");

      bool checkPassword = PasswordHelper.VerifyPassword(password, user.Password);
      if (checkPassword)
      {
        user.CountLogin = user.CountLogin == null ? 1 : user.CountLogin + 1;
        user.LastLogin = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return DataResponse.RespondSuccess(user);
      }

      return DataResponse.RespondFailure("Tài khoản không chính xác!");
    }
    catch
    {
      return DataResponse.RespondFailure("Tài khoản không chính xác!");
    }
  }

  /// <summary>
  /// Cập nhật mật khẩu cho User đang login
  /// </summary>
  /// <param name="password"></param>
  /// <param name="user"></param>
  /// <returns>bool</returns>
  public async Task<bool> ChangePasswordAsync(string password, User user)
  {
    string passwordHash = PasswordHelper.HashPassword(password);
    user.Password = passwordHash;
    user.UpdatedDate = DateTime.UtcNow;
    user.UpdatedBy = user.Id;
    try
    {
      await _userRepository.UpdateAsync(user);
      return true;
    } catch
    {
      return false;
    }
  }

  public async Task<IPagedList<User>> GetListStudentStudyingAsync(int page, int count)
  {
    if (page <= 0 || count <= 0)
    {
      page = 0;
      count = 10;
    }
    if (page > 0)
    {
      page--;
    }
    var specification = new UserFilterPaginatedSpecification(page, count, (int)UserType.Student);
    var itemsOnPage = await _userRepository.ListAsync(specification);

    var filterBy = new UserFilterPaginatedSpecification((int)UserType.Student);
    var totalItems = await _userRepository.CountAsync(filterBy);
    if (itemsOnPage != null && itemsOnPage.Count > 0 && totalItems > 0)
    {
      return new PagedList<User>(itemsOnPage, page, count, totalItems);
    }
    return new PagedList<User>(new List<User>(), 0, 0);
  }
}
