using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Entities.SettingAggregate;
public class CommonSettings : ISettings
{
  public string WebsiteUrl { get; set; } = string.Empty;
  public string SiteName { get; set; } = string.Empty;
  public string ResetPasswordUrl { get; set; } = string.Empty;
  public string ChangePasswordUrl { get; set; } = string.Empty;
  public string ApiUrl { get; set; } = string.Empty;
  public bool IsOnForSeller { get; set; }
  public string Slug { get; set; } = "https://xxxxxxxxxxx.com/xxxxxxxx/";
  public string ConfirmShippingUrl { get; set; } = string.Empty;

  public string Subdomain { get; set; } = string.Empty;

  public string Privacy { get; set; } = string.Empty;
  public string EmailReceiveInquiry { get; set; } = "fanmachi@hiromori.co.jp";
}
