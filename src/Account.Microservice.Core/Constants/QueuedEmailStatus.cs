using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum QueuedEmailStatus
{
  /// <summary>
  /// Low
  /// </summary>
  Wait = 0,

  /// <summary>
  /// High
  /// </summary>
  Sent = 10
}
