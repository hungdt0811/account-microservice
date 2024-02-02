using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Account.Microservice.Core.Constants;
using System.Threading;
using System.Net.NetworkInformation;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public sealed class UsersSpecification : Specification<User>
{
  /// <summary>
  /// Query User by userId
  /// </summary>
  /// <param name="userId"></param>
  public UsersSpecification(IList<int> lstUserId)
  {
    if (lstUserId.Count > 0)
    {
      Query
      .Where(b => lstUserId.Contains(b.Id));
    }
  }

  public UsersSpecification(string email, string token)
  {
    Query.Where(b => b.Email == email && b.RememberToken == token && b.Status == (int)UserStatus.Active);
  }

  public UsersSpecification(int status, bool getAll)
  {
    Query
        .Where(b => b.Status == status);
  }

  public UsersSpecification(bool? adminActive = null)
  {
    if (adminActive.HasValue)
    {
      Query
    .Where(r => r.Status == (int)UserStatus.Active && r.IsSystemRole == true);
    }

    Query
        .OrderByDescending(r => r.Id);
  }

  public UsersSpecification(string email)
  {
    Query
        .Where(b => b.Email == email);
  }

  public UsersSpecification(string email, int status)
  {
    Query
        .Where(b => b.Email == email && b.Status == status);
  }

  public UsersSpecification()
  {

  }
}
