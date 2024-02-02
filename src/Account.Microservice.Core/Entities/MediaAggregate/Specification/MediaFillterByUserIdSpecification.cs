using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Entities.MediaAggregate.Specification;
public class MediaFillterByUserIdSpecification : Specification<Media>
{
  public MediaFillterByUserIdSpecification(int page, int count, int? userId = null, bool selectOnlyImage = false, int? userAdminId = 0)
  {
    Query.Skip(page).Take(count);

    if (userId != null)
    {
      Query.Where(m => m.CreateUid == userId || (m.CreateUid == userAdminId && userAdminId != 0));
    }

    Query.Where(m => m.IsShow);
    if (selectOnlyImage)
    {
      Query.Where(m => m.MimeType.Contains("image/"));
    }
    Query.OrderByDescending(m => m.Id);
  }
  public MediaFillterByUserIdSpecification(int? userId = null, bool selectOnlyImage = false, int? userAdminId = 0)
  {
    if (userId != null)
    {
      Query.Where(m => m.CreateUid == userId || (m.CreateUid == userAdminId && userAdminId != 0));
    }

    Query.Where(m => m.IsShow);
    if (selectOnlyImage)
    {
      Query.Where(m => m.MimeType.Contains("image/"));
    }
  }
}
