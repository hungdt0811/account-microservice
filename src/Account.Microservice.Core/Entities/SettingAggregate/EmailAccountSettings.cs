using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Entities.SettingAggregate;
public class EmailAccountSettings: ISettings
{
  public string AddressEmail { get; set; } = string.Empty;
  public string Name { get; set; }= string.Empty;
  public string Host { get; set; } = string.Empty;
  public string UserName { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public int Port { get; set; } 
  public bool Ssl { get; set; } 

}
