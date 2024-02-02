using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum OrderStatus
{
  Create = 10,
  Processing = 20,
  Success = 30,
  Fail = 40,
  Cancel = 50,
  Refund = 60
}
