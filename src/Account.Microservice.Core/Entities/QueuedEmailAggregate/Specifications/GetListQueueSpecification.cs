using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Account.Microservice.Core.Constants;

namespace Account.Microservice.Core.Entities.QueuedEmailAggregate.Specifications;
public class GetListQueueSpecification : Specification<QueuedEmail>
{
  public GetListQueueSpecification()
  {
    const int count = 1000;
    const int maxSentTries = 3;
    Query.Where(b => b.Status == (int)QueuedEmailStatus.Wait && b.SentTries < maxSentTries).Take(count);
  }
}
