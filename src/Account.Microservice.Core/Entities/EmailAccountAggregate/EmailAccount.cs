using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;

namespace Account.Microservice.Core.Entities.EmailAccountAggregate;
public class EmailAccount : EntityBase, IAggregateRoot
{
  /// <summary>
  /// Address email from
  /// </summary>
  public string AddressEmail { set; get; } = string.Empty;

  /// <summary>
  /// display name
  /// </summary>
  public string Name { set; get; } = string.Empty;

  /// <summary>
  /// host
  /// </summary>
  public string Host { set; get; } = string.Empty;

  /// <summary>
  /// username account
  /// </summary>
  public string UserName { set; get; } =string.Empty;

  /// <summary>
  /// password
  /// </summary>
  public string Password { set; get; } = string.Empty;

  /// <summary>
  /// port send mail
  /// </summary>
  public int Port { set; get; }

  public bool Ssl { get; set; }

  public bool IsDefault { get; set; }
}
