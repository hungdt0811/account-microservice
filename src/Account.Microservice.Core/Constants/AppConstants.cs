using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public static class AppConstants
{
  public static readonly int[] Paging = { 10, 20, 50, 100 };
  public static readonly int LenghtSaltKeyDefault = 5;
  public const int AccessTokenExpirationSeconds = 60;

  public const int RefreshTokenExpirationSeconds = 1200;

  public const string TokenHeader = "Authorization";

  public const string ClientSecret = "ClientSecret";

  public const string ClientId = "ClientId";

  public const string FolderMedia = "Media";
  public const string RoleSystemAdmin = "System.Admin";
  public const string RoleSystemSeller = "System.Seller";
  public const string DefaultColorBar = "#C5D86D";

  public const string FreeInput = "フリー入力エリア";
  public const string Report = "レポート";
  public const string Comment = "応援コメント";
  public const string ContactUs = "お問合せ";
  public const string Other = "無し";
}
