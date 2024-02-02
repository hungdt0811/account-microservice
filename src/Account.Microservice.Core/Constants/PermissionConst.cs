using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public static class PermissionConst
{
  public const string Role = "role"; // Quản lý phân quyền - danh mục cha
  public const string RoleList = "role_list"; // Xem danh sách nhóm quyền
  public const string RoleAdd = "role_add"; // Thêm mới nhóm quyền
  public const string RoleEdit = "role_edit"; // Chỉnh sửa nhóm quyền
  public const string RoleAssign = "role_assign"; // Phân quyền cho tài khoản
  public const string RoleDelete = "role_delete"; // Xoá nhóm quyền

  public const string User = "user"; // Quản lý người dùng
  public const string UserListStudent = "user_list_student"; // Xem danh sách học viên
  public const string UserListCollab = "user_list_collab"; // Xem danh sách CTV
  public const string UserListLecturer = "user_list_lecturer"; // Xem danh sách giảng viên
  public const string UserAdd = "user_add"; // Thêm mới tài khoản
  public const string UserDetail = "user_detail"; // Xem chi tiết tài khoản
  public const string UserEdit = "user_edit"; // Chỉnh sửa thông tin tài khoản
  public const string UserDelete = "user_delete"; // Xoá tài khoản
}
