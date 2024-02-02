using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;

namespace Account.Microservice.Web.Services.Users;

public interface IEmailRelatedLocalService
{
  Task<BaseResponseModel> DeleteAsync(int id);
  Task<BaseResponseModel> GetAllAsync(int userId);
}
