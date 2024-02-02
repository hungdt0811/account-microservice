using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Entities.SecurityAggregate;
public interface IEncryptionService
{
  /// <summary>
  /// Create salt key
  /// </summary>
  /// <param name="size">Key size</param>
  /// <returns>Salt key</returns>
  string CreateSaltKey(int size);

  /// <summary>
  /// Create a password hash
  /// </summary>
  /// <param name="password">Password</param>
  /// <param name="saltKey">Salk key</param>
  /// <returns>Password hash</returns>
  string CreatePasswordHash(string password, string saltKey);
}
