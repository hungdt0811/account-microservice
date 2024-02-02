using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.SecurityAggregate.Specifications;
public sealed class RPSpecification : Specification<RolePermission>
{

  public RPSpecification(int roleId)
  {
    Query.Where(b => b.RoleId == roleId);
  }
}
