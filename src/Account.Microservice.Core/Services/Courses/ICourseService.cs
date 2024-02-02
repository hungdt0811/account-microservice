using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.CoursesAggreate;

namespace Account.Microservice.Core.Services.Courses;
public interface ICourseService
{
  #region CUD

  // Create
  Task<Course?> CreateHasInstructionsCourseAsync(string courseName, int courseStatus, int lectureId, int userCreatedId);
  Task<Course?> CreateSelfStudyCourseAsync(string courseName, int courseStatus, int lectureId, int userCreatedId);

  #endregion


}
