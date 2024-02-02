using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.CoursesAggreate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Helpers;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.Courses;
public class CourseService : ICourseService
{
  private readonly IRepository<Course> _courseRepository;
  private readonly IRepository<User> _userRepository;
  public CourseService(IRepository<Course> courseRepository, IRepository<User> userRepository)
  {
    _courseRepository = courseRepository;
    _userRepository = userRepository;
  }

  public async Task<Course?> CreateHasInstructionsCourseAsync(string courseName, int courseStatus, int lectureId, int userCreatedId)
  {
    var user = await _userRepository.GetByIdAsync(lectureId);
    if(user == null)
    {
      return null;
    }
    string slug = CommonHelper.CreateSlug(courseName);
    var course = new Course(courseName, slug, lectureId, courseStatus, (int)CourseType.Guild);
    course.Slug = slug;
    course.CreatedBy = userCreatedId;
    var courseAdded = await _courseRepository.AddAsync(course);
    return courseAdded;
  }

  public Task<Course?> CreateSelfStudyCourseAsync(string courseName, int courseStatus, int lectureId, int userCreatedId)
  {
    throw new NotImplementedException();
  }
}
