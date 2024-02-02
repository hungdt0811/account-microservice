using Ardalis.Specification.EntityFrameworkCore;
using Account.Microservice.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Account.Microservice.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }
  private IQueryable<T>? _entities;
  AppDbContext _dbContext;
  public virtual IQueryable<T> Table => _entities ?? (_entities = _dbContext.Set<T>());
}
