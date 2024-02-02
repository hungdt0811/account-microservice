using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.SharedKernel;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.CoursesAggreate;

namespace Account.Microservice.Core.Entities.UserAggregate;
public class User : EntityBase, IAggregateRoot
{
  #region Ctor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public User
  (
    string fullName,
    string email,
    string password
  )
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
    FullName = fullName;
    Email = email;
    Password = password;
  }

  #endregion

  #region Methods

  #endregion

  #region Properties

  public string Password { get; set; }
  public string FullName { get; set; }
  public string Email { get; set; }
  public int? Sex { get; set; }
  public string? Address { get; set; }
  public string? Mobile { get; set; }
  public DateTime? Birthday { get; set; }
  public int Status { get; set; } = (int)UserStatus.Active;
  public int Type { get; set; } = (int)UserType.Student;
  public string? EmailRef { get; set; }
  public int? CountLogin { get; set; }
  public DateTime? LastLogin { get; set; }

  public string CodeConfirm { get; set; } 

  public bool IsSystemRole { get; set; } = false;

  public string? Avatar { get; set; }

  public string RememberToken { get; set; } = string.Empty;
  #endregion

  public ICollection<Role>? Roles { get; set; }
  public ICollection<Course>? Courses { get; set; }
}
