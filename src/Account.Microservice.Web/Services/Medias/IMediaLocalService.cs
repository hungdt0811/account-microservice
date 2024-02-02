using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Medias;

namespace Account.Microservice.Web.Services.Medias;

public interface IMediaLocalService
{
  Task<BaseResponseModel> UploadAsync(MediaUploadModel mediaUploadModel);

  Task<BaseResponseModel> UploadS3Async(MediaUploadModel mediaUploadModel);

  Task<BaseResponseModel> ReadS3Async(string key);
  Task<BaseResponseModel> GetList(int page, int count,int userId, bool selectOnlyImage = false, int? sellerId = null);
  Task<BaseResponseModel> GetById(int id);
  Task<BaseResponseModel> DeleteAsync(int id);
}
