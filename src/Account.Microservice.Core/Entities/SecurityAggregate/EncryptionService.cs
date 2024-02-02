using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Entities.SecurityAggregate;
public class EncryptionService: IEncryptionService
{
  #region Fields

  #endregion

  #region Ctor

  public EncryptionService()
  {
  }

  #endregion

  #region Utilities

  #endregion

  #region Methods

  public virtual string CreateSaltKey(int size)
  {
    return BCrypt.Net.BCrypt.GenerateSalt(size);
  }

  public virtual string CreatePasswordHash(string password, string saltKey)
  {
    return BCrypt.Net.BCrypt.HashPassword(password, saltKey);
  }
  #endregion
}
