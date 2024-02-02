using System.Numerics;
using Ardalis.GuardClauses;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.SharedKernel;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Entities.UserAggregate;
public class MediaBinary : EntityBase, IAggregateRoot
{
  #region Ctor

  #endregion

  #region Propertises

  /// <summary>
  /// MediaId
  /// /// </summary>
  public byte BinaryData { get; set; }
  /// <summary>
  /// MediaId
  /// /// </summary>
  public int MediaId { get; set; }

  #endregion

}
