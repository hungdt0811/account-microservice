using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
class UserFilterOrderByDescSpecification : Specification<User>
    {
        //public UserFilterOrderByDescSpecification(string companyName, string branchName, string userName, int? roleId, int? branchId, int? companyId, int roleLevel = 0)
        //{

        //    Query.Where(i => i.IsActive);

        //    //if (!string.IsNullOrEmpty(companyName))
        //    //    Query.Where(i => i.Branch.Company.CompanyName.ToLower().Contains(companyName.ToLower()));
        //    //if (!string.IsNullOrEmpty(branchName))
        //    //    Query.Where(i => i.Branch.BranchName.ToLower().Contains(branchName.ToLower()));
        //    //if (!string.IsNullOrEmpty(userName))
        //    //    Query.Where(i => i.FamilyName.ToLower().Contains(userName.ToLower()) || i.GivenName.ToLower().Contains(userName.ToLower()) || (i.FamilyName.ToLower() + " " + i.GivenName.ToLower()).Contains(userName.ToLower()));
        //    if (roleId.HasValue && roleId > 0)
        //        Query.Where(i => i.RoleId == roleId);
        //    if (roleLevel != 0)
        //        Query.Where(i => i.Role.Level <= roleLevel);

        //    Query.OrderByDescending(x => x.CreateDate);
        //}
    }

