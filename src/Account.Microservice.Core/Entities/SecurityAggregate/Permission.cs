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
/// Quyền chức năng
/// </summary>
public partial class Permission : EntityBase, IAggregateRoot
{
  /// <summary>
  /// Gets or sets the permission name
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Gets or sets the permission system name
  /// </summary>
  public string? Code { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int? ParentId { get; set; }

  #region Relationship
  public virtual ICollection<Permission>? Children { get; set; }
  #endregion
}

