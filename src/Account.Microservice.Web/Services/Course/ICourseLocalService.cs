using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Course;


namespace Account.Microservice.Web.Services.Course;

public interface ICourseLocalService
{
  Task<BaseResponseModel> CreateHasInstructionsCourseAsync(CreateHasInstructionsCourseModel createCourseModel, int courseCreatedId);
  Task<BaseResponseModel> CreateSelfStudyCourseAsync(CreateSelfStudyCourseModel createCourseModel, int courseCreatedId);
}
