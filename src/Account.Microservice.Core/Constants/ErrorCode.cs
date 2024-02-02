using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum ErrorCode
{
  SystemError,
  Success,
  InvalidInput,
  NoData,
  ErrorExistEntity,
  ExpireResetPassword,
  WrongPassword,
  ExpireSession,
  NotAllow, 
  UploadFail,
  Deleted,
  NotFound = 404
}
