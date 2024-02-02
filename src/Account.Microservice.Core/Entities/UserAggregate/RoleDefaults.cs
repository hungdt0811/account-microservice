using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Entities.UserAggregate;
public static class RoleDefaults
{
  #region Role Name
  public static string SystemAdministratorRoleName => "SystemAdministrator";

  public static string SupportAdministratorRoleName => "SupportAdministrator";

  public static string HeadOfficeRoleName => "HeadOffice";

  public static string BranchManagerRoleName => "BranchManager";

  public static string StaffRoleName => "Staff";

  public static string OwnerRoleName => "Owner";
  #endregion
}
