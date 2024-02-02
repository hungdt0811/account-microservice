using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Entities.EmailAccountAggregate.Specifications;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.EmailAccounts;
public class EmailAccountService : IEmailAccountService
{
  private readonly IRepository<EmailAccount> _emailAccountRepository;
  public EmailAccountService(IRepository<EmailAccount> emailAccountRepository)
  {
    _emailAccountRepository = emailAccountRepository;
  }
  public Task<EmailAccount> CreateEmailAccountAsync(string name, string emailAddress, string userName, string password, string host, int port, bool ssl, bool isDefault)
  {
    var emailAccount = new EmailAccount
    {
      Name = name,
      AddressEmail= emailAddress,
      UserName = userName, 
      Host= host,
      Port = port,
      Password= password,
      Ssl= ssl,
      IsDefault= isDefault
    };
    return _emailAccountRepository.AddAsync(emailAccount);
  }

  public Task DeleteEmailAccountAsync(int emailAccountId)
  {
    throw new NotImplementedException();
  }

  public async Task<EmailAccount?> GetEmailAccountDefaultAsync()
  {
    var defaultSpecification = new EmailAccountDefault();
    return await _emailAccountRepository.FirstOrDefaultAsync(defaultSpecification);
  }
}
