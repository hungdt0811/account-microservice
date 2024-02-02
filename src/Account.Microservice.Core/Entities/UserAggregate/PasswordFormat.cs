using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Entities.UserAggregate;
public enum PasswordFormat
{
  Md5Hashed = 1,
  Sha1Hashed = 2,
  Sha256Hashed = 3
}
