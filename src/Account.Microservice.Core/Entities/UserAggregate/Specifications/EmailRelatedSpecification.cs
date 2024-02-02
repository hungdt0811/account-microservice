using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public class EmailRelatedSpecification : Specification<EmailRelated>
{
  public EmailRelatedSpecification(int id)
  {
    Query.Where(e => e.Id == id);
  }
  public EmailRelatedSpecification(int userId, bool includeUser)
  {
    Query.Where(e => e.UserId == userId).Include(e => e.User);
  }
  public EmailRelatedSpecification(int userId, string email)
  {
    Query.Where(e => e.UserId == userId && e.Email == email).Include(e => e.User);
  }
}
