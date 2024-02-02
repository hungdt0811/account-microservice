using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;
using Ardalis.GuardClauses;
using System.Data;
namespace Account.Microservice.Core.Entities.SecurityAggregate;

/// <summary>
/// Chi tiết nhóm quyền
/// </summary>
public class RolePermission : EntityBase, IAggregateRoot
{
  /// <summary>
  /// Gets or sets the RoleId
  /// </summary>
  public int RoleId { get; set; }
  /// <summary>
  /// Gets or sets the PermissionId
  /// </summary>
  public int PermissionId { get; set; }


  #region Relationship
  public Role? Role { get; set; }

  public Permission? Permission { get; set; }
  #endregion
}
