using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Entities.EmailTemplateAggregate;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Services.EmailTemplates;
public interface IEmailTemplateService
{
  Task DeleteAsync(int emailAccountId);
  Task<EmailTemplate> GetBySystemNameAsync(string systemName);

}
