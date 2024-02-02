using Account.Microservice.Core.Constants;
using Ardalis.Specification;
namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public class UserFilterPaginatedSpecification : Specification<User>
{
  public UserFilterPaginatedSpecification(int page, int count, int type)
  {
    Query.Skip(page * count).Take(count);
    Query.Where(u => u.Status == (int)UserStatus.Deactive && u.Type == type);
    Query.OrderByDescending(u => u.Id);
  }

  public UserFilterPaginatedSpecification(int type)
  {
    Query.Where(u => u.Status == (int)UserStatus.Deactive && u.Type == type);
  }

}
