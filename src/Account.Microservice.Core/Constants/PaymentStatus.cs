using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum PaymentStatus
{
  Pending = 10,
  Processing = 20,
  Success = 30,
  Refund = 40,
  Cancel = 50,
}
