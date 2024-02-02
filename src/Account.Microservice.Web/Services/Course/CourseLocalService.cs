using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Services.Courses;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Course;

namespace Account.Microservice.Web.Services.Course;

public class CourseLocalService : ICourseLocalService
{
  private readonly ICourseService _courseService;
  public CourseLocalService(ICourseService courseService) 
  {  
    _courseService = courseService; 
  }
  public async Task<BaseResponseModel> CreateHasInstructionsCourseAsync(CreateHasInstructionsCourseModel model, int userCreatedId)
  {
    var course = await _courseService.CreateHasInstructionsCourseAsync(model.Name, model.Status, model.LecturerId, userCreatedId);
    if(course == null)
    {
      return ResponseModel.RespondFailure(ErrorCode.NoData, "Tạo khóa học thất bại");
    }
    return ResponseModel.RespondSuccess(course);
  }

  public Task<BaseResponseModel> CreateSelfStudyCourseAsync(CreateSelfStudyCourseModel createCourseModel, int userCreatedId)
  {
    throw new NotImplementedException();
  }
}
