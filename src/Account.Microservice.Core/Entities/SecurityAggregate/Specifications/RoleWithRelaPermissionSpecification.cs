using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.SecurityAggregate.Specifications;
public sealed class RoleWithRelaPermissionSpecification : Specification<Role>
{
  public RoleWithRelaPermissionSpecification(int id)
  {
    Query.Where(b => b.Id == id).Include(p => p.Permissions);
  }

  public RoleWithRelaPermissionSpecification(List<int> ids)
  {
    Query.Where(b => ids.Contains(b.Id)).Include(p => p.Permissions);
  }
}
