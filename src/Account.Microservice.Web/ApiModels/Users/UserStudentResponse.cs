using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Web.ViewModels;

namespace Account.Microservice.Web.ApiModels.Users;

public class UserStudentResponse
{
  public int Id { get; set; }
  public DateTime? CreatedDate { get; set; }
  public string? Email { get; set; }
  public string? FullName { get; set; }
  public string? Mobile { get; set; }
  public string? Avatar { get; set; }
  public string? EmailRef { get; set; }
  public string? LecturerName { get; set; }
  public string? CourseName { get; set; }
  public string? Code { get; set; }
  public string? CaregiverName { get; set; }
  public DateTime? AttendanceDate { get; set; }
  public float? TotolPaid { get; set; }

  public UserStudentResponse ToModel(User user)
  {
    return new UserStudentResponse
    {
      Id = user.Id,
      Email = user.Email,
      FullName = user.FullName,
      Avatar = user.Avatar,
      EmailRef = user.EmailRef,
      Mobile = user.Mobile,
      CreatedDate = user.CreatedDate,
    };
  }
}

