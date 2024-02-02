using Ardalis.Specification;
namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public sealed class UserFilterSpecification : Specification<User>
    {
        //public UserFilterSpecification(int? activeStatusFilterId, string? companyName, string? branchName, string userName, int? roleId, int? branchId, int? companyId, int roleLevel = 0)
        //{
        //    if (activeStatusFilterId.HasValue)
        //    {
        //        bool isActiveSet = activeStatusFilterId == 1;
        //        Query.Where(i => i.IsActive == isActiveSet);
        //    }
        //    if (!string.IsNullOrEmpty(userName))
        //        Query.Where(i => i.FirstName.ToLower().Contains(userName.ToLower()) || i.LastName!.ToLower().Contains(userName.ToLower()) || (i.FirstName.ToLower() + " " + i.LastName!.ToLower()).Contains(userName.ToLower()));
        //    if (roleId.HasValue && roleId > 0)
        //        Query.Where(i => i.RoleId == roleId);
        //    if (roleLevel != 0)
        //        Query.Where(i => i.Role.Level <= roleLevel);

        //}
    }
