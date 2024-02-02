using Account.Microservice.Web.ApiModels;

namespace Account.Microservice.Web.Services.Settings;

public interface ISettingLocalService
{
  Task<BaseResponseModel> UpdateOnforSellerAsync(bool isOnforSeller);
  BaseResponseModel GetPrivacyAsync();
  Task<BaseResponseModel> UpdatePrivacy(string privacy);
}
