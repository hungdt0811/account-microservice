using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.SharedKernel;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Entities.SecurityAggregate;
public class UserRole : EntityBase, IAggregateRoot
{
  /// <summary>
  /// Gets or sets the UserId
  /// </summary>
  public int UserId { get; set; }
  /// <summary>
  /// Gets or sets the RoleId
  /// </summary>
  public int RoleId { get; set; }

  #region Relationship
  public User? User { get; set; }
  public Role? Role { get; set; }
  #endregion
}
