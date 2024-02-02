using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailTemplateAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Entities.UserAggregate.Specifications;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.EmailTemplates;
public class EmailTemplateService : IEmailTemplateService
{
  private readonly IRepository<EmailTemplate> _emailTemplateService;
  public EmailTemplateService(IRepository<EmailTemplate> emailTemplateService)
  {
    _emailTemplateService = emailTemplateService;
  }

  public Task DeleteAsync(int emailAccountId)
  {
    throw new NotImplementedException();
  }

  public async Task<EmailTemplate> GetBySystemNameAsync(string systemName)
  {
    var system = new BySystemName(systemName);
    var emailTemplate = await  _emailTemplateService.FirstOrDefaultAsync(system);
    return emailTemplate!;
  }
}
