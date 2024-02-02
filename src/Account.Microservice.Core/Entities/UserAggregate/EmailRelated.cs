using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;

namespace Account.Microservice.Core.Entities.UserAggregate;
public class EmailRelated : EntityBase, IAggregateRoot
{
  public int UserId { get; set; }
  public string Email { get; set; } = string.Empty;
  public User? User { get; set; }
}
