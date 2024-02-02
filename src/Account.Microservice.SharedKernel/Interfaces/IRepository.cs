using System.Linq.Expressions;
using Ardalis.Specification;
namespace Account.Microservice.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
  IQueryable<T> Table { get; }
}
