using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Services.EmailAccounts;
public interface IEmailAccountService
{
  Task<EmailAccount> CreateEmailAccountAsync(string name, string emailAddress,string userName, string password, string host, int port, bool ssl, bool isDefault);

  Task DeleteEmailAccountAsync(int emailAccountId);
  Task<EmailAccount?> GetEmailAccountDefaultAsync();

}
