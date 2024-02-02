using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Entities.UserAggregate.Specifications;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.Users;
public class EmailRelatedService : IEmailRelatedService
{
  public readonly IRepository<EmailRelated> _repositoryEmailRelated;

  public EmailRelatedService(IRepository<EmailRelated> repositoryEmailRelated)
  {
    _repositoryEmailRelated = repositoryEmailRelated;
  }

  public async Task<EmailRelated> CreateAsync(int userId, string email)
  {
    var emailRelated = new EmailRelated
    {
      UserId = userId,
      Email = email
    };
    return await _repositoryEmailRelated.AddAsync(emailRelated);
  }

  public async Task DeleteAsync(int id)
  {
    var specification = new EmailRelatedSpecification(id);
    var email = await _repositoryEmailRelated.FirstOrDefaultAsync(specification);
    await _repositoryEmailRelated.DeleteAsync(email!);
  }

  public async Task DeleteRangeAsync(int userId)
  {
    var specification = new EmailRelatedSpecification(userId, true);
    var listEmail = await _repositoryEmailRelated.ListAsync(specification);
    await _repositoryEmailRelated.DeleteRangeAsync(listEmail);
  }

  public async Task<IEnumerable<EmailRelated>> GetAllAsync(int userId)
  {
    var specification = new EmailRelatedSpecification(userId, true);
    return await _repositoryEmailRelated.ListAsync(specification);
  }

  public async Task<EmailRelated?> GetById(int userId, string email)
  {
    var specification = new EmailRelatedSpecification(userId, email);
    return await _repositoryEmailRelated.FirstOrDefaultAsync(specification);
  }

  public async Task UpdateAsync(EmailRelated emailRelated)
  {
    await _repositoryEmailRelated.UpdateAsync(emailRelated);
  }
}
