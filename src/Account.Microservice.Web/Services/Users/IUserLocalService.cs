using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;

namespace Account.Microservice.Web.Services.Users;

public interface IUserLocalService
{
  Task<BaseResponseModel> CreateUserAsync(CreateUserModel createUserModel, int userCreatedId);
  Task<BaseResponseModel> LoginUserCms(LoginModel loginModel);
  Task<BaseResponseModel> ChangePasswordAsync(ChangePasswordModel model, User user);
  Task<BaseResponseModel> UpdateProfileAsync(UpdateProfileModel updateProfileModel, User user);
  Task<IPagedList<UserStudentResponse>> GetListStudentStudyingAsync(int page, int count);
}
