using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.ProjectAggregate;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Setting;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Services.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Account.Microservice.Web.Api;
public class SettingsController : BaseApiController
{
  private readonly ISettingLocalService _settingLocalService;

  public SettingsController(ISettingLocalService settingLocalService)
  {
    _settingLocalService = settingLocalService;
  }

  [Authorize]
  [HttpPut("common/onforseller")]
  public async Task<IActionResult> Update(bool isOnForSeller)
  {
    
    if (!ModelState.IsValid)
    {
      return StatusCode(StatusCodes.Status400BadRequest, ModelState);
    }
    var model = await _settingLocalService.UpdateOnforSellerAsync(isOnForSeller);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest, model);
    }
    return Ok(model);
  }

  [Authorize]
  [HttpGet("privacy")]
  public IActionResult Get()
  {
    var model =  _settingLocalService.GetPrivacyAsync();
    return Ok(model);
  }

  [Authorize]
  [HttpPut("update-privacy")]
  public async Task<IActionResult> Update(PrivacyModel privacyModel)
  {
    if (!ModelState.IsValid)
    {
      return StatusCode(StatusCodes.Status400BadRequest, ModelState);
    }

    var model = await _settingLocalService.UpdatePrivacy(privacyModel.Privacy);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest, model);
    }
    return Ok(model);
  }
}
