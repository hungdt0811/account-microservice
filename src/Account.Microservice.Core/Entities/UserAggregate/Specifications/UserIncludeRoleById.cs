using Ardalis.Specification;
namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public sealed class UserIncludeRoleById : Specification<User>
{
  /// <summary>
  /// Query user include Role
  /// </summary>
  /// <param name="id"></param>
  public UserIncludeRoleById(int id)
  {
    Query.Where(b => b.Id == id).Include(b => b.Roles);
  }
}
