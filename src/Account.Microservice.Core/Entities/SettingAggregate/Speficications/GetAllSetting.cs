using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.SettingAggregate.Speficications;
public class GetAllSetting: Specification<Setting>
{
  public GetAllSetting()
  {
    Query.OrderBy(b=>b.Id);
  }
}
