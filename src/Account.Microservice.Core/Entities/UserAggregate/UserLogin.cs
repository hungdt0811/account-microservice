using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Constants;

namespace Account.Microservice.Core.Entities.UserAggregate;
public class UserLogin
{
  public UserLogin
  (
    string fullName,
    string email
  )
  {
    FullName = fullName;
    Email = email;
  }
  public int Id { get; set; }
  public string FullName { get; set; }
  public string Email { get; set; }
  public string? Avatar { get; set; }
}
