using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.Services.Settings;
using Account.Microservice.Web.ApiModels;

namespace Account.Microservice.Web.Services.Settings;

public class SettingLocalService : ISettingLocalService
{
  private readonly ILogger<Setting> _logger;
  private readonly CommonSettings _commonSettings;
  private readonly ISettingService _settingService;

  public SettingLocalService(CommonSettings commonSettings, ISettingService settingService, ILogger<Setting> logger)
  {
    _commonSettings = commonSettings;
    _settingService = settingService;
    _logger = logger;
  }

  public async Task<BaseResponseModel> UpdateOnforSellerAsync(bool isOnforSeller)
  {
    try
    {
      _commonSettings.IsOnForSeller = isOnforSeller;
      await _settingService.SaveSettingAsync(_commonSettings);
      return ResponseModel.RespondSuccess();
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error when update OnforSeller {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError);
    }
  }

  public BaseResponseModel GetPrivacyAsync()
  {
    try
    {
      var privacy = _commonSettings.Privacy;
      return ResponseModel.RespondSuccess(new { privacy = privacy });
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error when GetPrivacyAsync {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError);
    }
  }

  public async Task<BaseResponseModel> UpdatePrivacy(string privacy)
  {
    try
    {
      _commonSettings.Privacy = privacy;
      await _settingService.SaveSettingAsync(_commonSettings);
      return ResponseModel.RespondSuccess();
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error when GetPrivacyAsync {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError);
    }
  }
}
