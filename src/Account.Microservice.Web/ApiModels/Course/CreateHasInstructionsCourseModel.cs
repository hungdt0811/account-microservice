using Account.Microservice.Core.Constants;

namespace Account.Microservice.Web.ApiModels.Course;

public class CreateHasInstructionsCourseModel
{
  #region Ctor
  public CreateHasInstructionsCourseModel
  (
    string name,
    int lecturerId,
    int status = (int)CourseStatus.NotActive
  )
  {
    Name = name;
    Status = status;
    LecturerId = lecturerId;
  }
  #endregion

  #region Properties
  public string Name { get; set; }
  public int Status { get; set; }
  public int LecturerId { get; set; }
  
  #endregion
}
