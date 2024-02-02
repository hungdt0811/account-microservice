using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.SecurityAggregate.Specifications;
public sealed class PermissionSpecification : Specification<Permission>
{
  public PermissionSpecification()
  {
    Query
        .Where(b => b.ParentId == null);

    Query.Include(b => b.Children);
  }
}
