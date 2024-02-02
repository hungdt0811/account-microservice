using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum UserStatus
{
  [Description("Vô hiệu")]
  Deactive,
  [Description("Kích hoạt")]
  Active,
  //[Description("Chờ kích hoạt")]
  //WaitConfirm,
  //[Description("Xoá")]
  //Delete
}
