using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.SecurityAggregate.Specifications;
public sealed class RoleSpecification : Specification<Role>
{
  public RoleSpecification()
  {
    Query.OrderBy(b => b.Id);

  }

  public RoleSpecification(int id)
  {
    Query.Where(b => b.Id == id);
  }
}
