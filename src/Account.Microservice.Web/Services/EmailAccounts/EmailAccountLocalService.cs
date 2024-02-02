using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Services.EmailAccounts;

namespace Account.Microservice.Web.Services.EmailAccounts;

public class EmailAccountLocalService : IEmailAccountLocalService
{
  private readonly IEmailAccountService _emailAccountService;
  public EmailAccountLocalService(IEmailAccountService emailAccountService)
  {
    _emailAccountService = emailAccountService;
  }
  public async Task<EmailAccount> GetEmailAccountDefault()
  {
    var email = await _emailAccountService.GetEmailAccountDefaultAsync();
    return email!;
  }
}
