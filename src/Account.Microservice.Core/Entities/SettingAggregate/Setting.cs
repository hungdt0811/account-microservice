using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;

namespace Account.Microservice.Core.Entities.SettingAggregate;
public class Setting: EntityBase, IAggregateRoot
{
  /// <summary>
  /// Gets or sets the name
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the value
  /// </summary>
  public string Value { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the store for which this setting is valid. 0 is set when the setting is for all stores
  /// </summary>
  public int CompanyId { get; set; }

  /// <summary>
  /// To string
  /// </summary>
  /// <returns>Result</returns>
  public override string ToString()
  {
    return Name;
  }
}
