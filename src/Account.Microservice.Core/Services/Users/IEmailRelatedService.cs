using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Services.Users;
public interface IEmailRelatedService
{
  Task<EmailRelated> CreateAsync(int userId, string email);
  Task UpdateAsync(EmailRelated emailRelated);
  Task DeleteAsync(int id);
  Task DeleteRangeAsync(int userId);
  Task<IEnumerable<EmailRelated>> GetAllAsync(int userId);
  Task<EmailRelated?> GetById(int userId, string email);
}
