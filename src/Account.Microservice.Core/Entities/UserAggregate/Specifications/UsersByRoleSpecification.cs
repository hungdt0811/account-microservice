using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public class UsersByRoleSpecification : Specification<User>
{
  public UsersByRoleSpecification(int role)
  {
    //Query.Where(u => u.RoleId == role);
  }
}
