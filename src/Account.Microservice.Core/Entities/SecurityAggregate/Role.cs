using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;
using Ardalis.GuardClauses;
using System.Data;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Entities.SecurityAggregate;

/// <summary>
/// Nhóm quyền
/// </summary>
public class Role : EntityBase, IAggregateRoot
{
  public Role(string roleName, bool isActive = false, string? note = null)
  {
    RoleName = roleName;
    IsActive = isActive;
    Note = note;
  }


  /// <summary>
  /// Gets or sets the RoleName
  /// </summary>
  public string RoleName { get; set; }

  /// <summary>
  /// System Name
  /// </summary>
  //public string SystemName { get; set; }

  /// <summary>
  /// Gets or sets the IsActive
  /// </summary>
  public bool IsActive { get; set; }

  /// <summary>
  /// Gets or sets the note
  /// </summary>
  public string? Note { get; set; }


  public ICollection<Permission>? Permissions { get; set; }
}

