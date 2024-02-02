namespace Account.Microservice.Web.ApiModels.Role;

public class CreateRoleModel
{
  public CreateRoleModel(string roleName, string permissionIds, bool isActive = false, string? note = null)
  {
    RoleName = roleName;
    PermissionIds = permissionIds;
    IsActive = isActive;
    Note = note;
  }

  public string RoleName { get; set; }

  /// <summary>
  /// Gets or sets the permission IDs for the role. This should be a JSON string representing an array of permission IDs.
  /// VD: "[7, 8, 9]"
  /// </summary>
  public string PermissionIds { get; set; }
  public bool IsActive { get; set; } = false;
  public string? Note { get; set; }
}
