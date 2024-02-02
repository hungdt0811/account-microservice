using Account.Microservice.Core.Constants;

namespace Account.Microservice.Web.ApiModels.Course;

public class CreateSelfStudyCourseModel
{
  #region Ctor
  public CreateSelfStudyCourseModel
  (
    string name,
    string code,
    int lecturerId,
    int status = (int)CourseStatus.NotActive,
    int type = (int)CourseType.Free
  )
  {
    Name = name;
    Code = code;
    Status = status;
    Type = type;
    LecturerId = lecturerId;
  }
  #endregion

  #region Properties
  public string Code { get; set; }
  public string Name { get; set; }
  public int Status { get; set; }
  public string? ImgPath { get; set; }
  public string? Summary { get; set; }
  public int Type { get; set; }
  public float Price { get; set; } = 0;
  public float OldPrice { get; set; } = 0;
  public string? OverviewDescription { get; set; }
  public int CategoryId { get; set; }
  public int Level { get; set; } = (int)CourseLevel.Primary;
  public int Rating { get; set; } = 0;
  public int LecturerId { get; set; }
  public string? Language { get; set; }
  public int IsCertificate { get; set; } = 0;
  public string? IntroVideo { get; set; }
  public string? ImgBanner { get; set; }
  public int TotalTimeVideo { get; set; } = 0;
  public string? Slug { get; set; }

  #endregion
}
