using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Entities.EmailTemplateAggregate;
using Account.Microservice.Core.Entities.QueuedEmailAggregate;
using Account.Microservice.Core.Entities.QueuedEmailAggregate.Specifications;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.QueuedEmails;
public class QueuedEmailService : IQueuedEmailService
{
  private readonly IRepository<QueuedEmail> _queuedEmailRepository;
  public QueuedEmailService(IRepository<QueuedEmail> queuedEmailRepository)
  {
    _queuedEmailRepository= queuedEmailRepository;
  }
  public async Task<QueuedEmail> CreateAsync(string from, string fromName, string to, string subject, string body, bool isBodyHtml, int retry)
  {
    var emailQueue = new QueuedEmail(from, fromName, to, subject, body, isBodyHtml);
    return await _queuedEmailRepository.AddAsync(emailQueue);
  }

  public async Task<List<QueuedEmail>> GetListQueue()
  {
    var listSpefication = new GetListQueueSpecification();
    var lst = await _queuedEmailRepository.ListAsync(listSpefication);
    return lst;
  }

  public async Task UpdateAsync(QueuedEmail queuedEmail)
  {
     await _queuedEmailRepository.UpdateAsync(queuedEmail);
  }
}
