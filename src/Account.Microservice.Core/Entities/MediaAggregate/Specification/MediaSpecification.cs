using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Entities.MediaAggregate.Specification;
public class MediaSpecification : Specification<Media>
{
  public MediaSpecification(int id)
  {
    Query.Where(m => m.Id == id);
  }
}
