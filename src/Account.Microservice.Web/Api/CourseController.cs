using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Services.Courses;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Course;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Services.Course;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Account.Microservice.Web.Api;

public class CourseController : BaseApiController
{
  private readonly ICourseService _courseService;
  private readonly ICourseLocalService _courseLocalService;
  private readonly IJwtUtils _jwtUtils;
  public CourseController
  (
    ICourseService courseService,
    ICourseLocalService courseLocalService,
    IJwtUtils jwtUtils
  )
  {
    _courseService = courseService;
    _courseLocalService = courseLocalService;
    _jwtUtils = jwtUtils;
  }

  [Authorize]
  [HttpGet]
  public IActionResult GetCourseAll()
  {
    return Ok(new BaseResponseModel
    {
      Success = false,
    });
  }

  [Authorize]
  [HttpPost("HasInstructionsCourse")]
  public async Task<IActionResult> CreateHasInstructionsCourseAsync(CreateHasInstructionsCourseModel createCousreModel)
  {
    int userCreateId = UserId(_jwtUtils);
    var result = await _courseLocalService.CreateHasInstructionsCourseAsync(createCousreModel, userCreateId);

    if(!result.Success)
    {
      return Ok(new BaseResponseModel 
      { 
        Success = false, 
        ErrorMessages = result.ErrorMessages 
      });
    }
    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = result.Data
    });
  }

  [Authorize]
  [HttpPost("SelfStudyCourse")]
  public async Task<IActionResult> CreateSelfStudyCourseAsync(CreateSelfStudyCourseModel createCousreModel)
  {
    int userCreateId = UserId(_jwtUtils);
    var result = await _courseLocalService.CreateSelfStudyCourseAsync(createCousreModel, userCreateId);

    if (!result.Success)
    {
      return Ok(new BaseResponseModel
      {
        Success = false,
        ErrorMessages = result.ErrorMessages
      });
    }
    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = result.Data
    });
  }

  //[Authorize]
  //[HttpPost]
  //public IActionResult PostSelfStudyCourse()
  //{
  //  return Ok(new BaseResponseModel
  //  {
  //    Success = false,
  //  });
  //}

}


