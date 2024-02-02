namespace Account.Microservice.Web.ApiModels.Users;

public class UserModel
{

  public int Id { get; set; }
  public int? RoleId { get; set; }

  public bool IsAdmin { get; set; }

  public string SellerName { get; set; } = string.Empty;

  /// <summary>
  /// Code 
  /// </summary>
  public string Code { get; set; } = string.Empty;

  /// <summary>
  /// Name
  /// </summary>
  public string FirstName { get; set; } = string.Empty;

  /// <summary>
  /// First Name
  /// </summary>
  public string LastName { get; set; } = string.Empty;

  /// <summary>
  /// Phone
  /// </summary>
  public string Phone { get; set; } = string.Empty;

  /// <summary>
  /// Email
  /// </summary>
  public string Email { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
  public string RememberToken { get; set; } = string.Empty;
  public bool IsRememberMe { get; set; }

  public int? MediaId { get; set; }

  public string Logo { get; set; } = "";
}
