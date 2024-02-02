using Ardalis.Specification;
using Account.Microservice.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public class UsersBySecretCodeSpecification : Specification<User>
{
  public UsersBySecretCodeSpecification(string code)
  {
    //Query.Where(b => b.Code == code && b.Status ==(int)UserStatus.Active);
  }
}
