using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;
using Ardalis.GuardClauses;
using System.Data;

namespace Account.Microservice.Core.Entities.EmailTemplateAggregate;

/// <summary>
/// Nhóm người dùng
/// </summary>
public class EmailTemplate: EntityBase, IAggregateRoot
{
  /// <summary>
  /// code for email template
  /// </summary>
  public string SystemName { get; set; } = string.Empty;

  /// <summary>
  /// subject of email template
  /// </summary>
  public string EmailSubject { get; set; } = string.Empty;

  /// <summary>
  /// content mail template is html fomat
  /// </summary>
  public bool ContentIsHtml { get; set; }

  /// <summary>
  /// content of email template
  /// </summary>
  public string Content { get; set; } = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  public bool Enable { get; set; }

  /// <summary>
  /// create date of email template
  /// </summary>
  //public DateTime CreatedDate { get; set; } = DateTime.Now;

}



