using Ardalis.Specification;
namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public sealed class UserFilterOrderByIdSpecification : Specification<User>
    {
        //public UserFilterOrderByIdSpecification(int? activeStatusFilterId, string? companyName, string? branchName, string userName, int? roleId, int? branchId, int? companyId, int roleLevel = 0)
        //{
        //    if (activeStatusFilterId.HasValue)
        //    {
        //        bool isActiveSet = activeStatusFilterId == 1;
        //        Query.Where(i => i.IsActive == isActiveSet);
        //    }
        //    //if (!string.IsNullOrEmpty(companyName))
        //    //    Query.Where(i => i.Branch.Company.CompanyName.ToLower().Contains(companyName.ToLower()));
        //    //if (!string.IsNullOrEmpty(branchName))
        //    //    Query.Where(i => i.Branch.BranchName.ToLower().Contains(branchName.ToLower()));
        //    //if (!string.IsNullOrEmpty(userName))
        //    //    Query.Where(i => i.FamilyName.ToLower().Contains(userName.ToLower()) || i.GivenName.ToLower().Contains(userName.ToLower()) || (i.FamilyName.ToLower() + " " + i.GivenName.ToLower()).Contains(userName.ToLower()));
        //    //if (roleId.HasValue && roleId > 0)
        //    //    Query.Where(i => i.RoleId == roleId);
        //    //if (branchId.HasValue && branchId > 0)
        //    //    Query.Where(i => i.BranchId == branchId);
        //    //if (companyId.HasValue && companyId > 0)
        //    //    Query.Where(i => i.Branch.CompanyId == companyId);
        //    if (roleLevel != 0)
        //        Query.Where(i => i.Role.Level <= roleLevel);

        //    Query.OrderBy(x => x.Id);
        //}
    }
