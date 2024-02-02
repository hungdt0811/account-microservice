using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public class UserListFilterPaging : Specification<User>
{
  //public UserListFilterPaging(int skip, int take, int roleId, string keySearch, string sortBy, bool ascending, int? status)
  // {
  //  if (!string.IsNullOrEmpty(keySearch))
  //  {
  //    if (!keySearch.Contains(","))
  //    {
  //      Query
  //        .Where(b => b.Email.ToLower().Contains(keySearch) ||
  //        b.FirstName.ToLower().Contains(keySearch) ||
  //        //b.LastName!.Contains(keySearch) ||
  //        b.CompanyName.ToLower().Contains(keySearch));
  //    }
  //    else
  //    {
  //      var listSearch = keySearch.Split(',', StringSplitOptions.TrimEntries).Where(x => !string.IsNullOrEmpty(x)).ToList();
  //      Query
  //        .Where(b => listSearch.Contains(b.Email) ||
  //        listSearch.Contains(b.FirstName) ||
  //        listSearch.Contains(b.CompanyName));
  //    }
  //  }
  //  Query.Where(i => i.RoleId == roleId);
  //  if (!string.IsNullOrEmpty(sortBy))
  //  {
  //    switch (sortBy)
  //    {
  //      case "email": if (ascending) { Query.OrderBy(x => x.Email); } else { Query.OrderByDescending(x => x.Email); }; break;
  //      case "createdDate": if (ascending) { Query.OrderByDescending(x => x.CreateDate); } else { Query.OrderBy(x => x.CreateDate); }; break;
  //      case "companyName": if (ascending) { Query.OrderBy(x => x.CompanyName); } else { Query.OrderByDescending(x => x.CompanyName); }; break;
  //      default: Query.OrderBy(x => x.Id); break;
  //        //case "totalNumberProduct": if (ascending) { Query.OrderBy(x => x.Email); } else { Query.OrderByDescending(x => x.Email); }; break;
  //        //case "totalNumberProductActive": if (ascending) { Query.OrderBy(x => x.Email); } else { Query.OrderByDescending(x => x.Email); }; break;
  //    }
  //  }
  //  else
  //  {
  //    Query.OrderByDescending(x => x.CreateDate);
  //  }
  //  if (status.HasValue)
  //  {
  //    Query.Where(i => i.Status == status.Value);
  //  }

  //  Query.Skip(skip).Take(take);
  //}
}
