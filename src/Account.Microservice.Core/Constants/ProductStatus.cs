using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum ProductStatus
{
  [Description("New")]
  New,
  [Description("Save")]
  Save,
  [Description("Publish")]
  Publish,
  [Description("Stop")]
  Stop,
  [Description("Delete")]
  Delete,
  [Description("Complete")]
  Complete,
  [Description("Not Reached")]
  NotReached,
  [Description("Preview")]
  Preview,
  [Description("Done")]
  Done = 10,
}
