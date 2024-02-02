using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.QueuedEmailAggregate;

namespace Account.Microservice.Core.Services.QueuedEmails;
public interface IQueuedEmailService
{
  Task<QueuedEmail> CreateAsync(string from, string fromName, string to, string subject, string body, bool isBodyHtml, int retry);
  Task<List<QueuedEmail>> GetListQueue();
  Task UpdateAsync(QueuedEmail queuedEmail);
}
