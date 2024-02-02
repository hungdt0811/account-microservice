using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Services.Users;
public interface ICodeGeneratorService
{
  string GenerateCode(int id);
  string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
    int passwordSize);
}
