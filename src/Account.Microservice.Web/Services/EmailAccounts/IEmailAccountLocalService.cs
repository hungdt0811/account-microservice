using Account.Microservice.Core.Entities.EmailAccountAggregate;

namespace Account.Microservice.Web.Services.EmailAccounts;

public interface IEmailAccountLocalService
{
  Task<EmailAccount> GetEmailAccountDefault();
}
