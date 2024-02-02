using System.Numerics;
using Ardalis.GuardClauses;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.SharedKernel;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Entities.UserAggregate;
public class Media : EntityBase, IAggregateRoot
{
  #region Ctor

  #endregion

  #region Propertises

  /// <summary>
  /// Mime Type
  /// </summary>
  public string MimeType { get; set; } = string.Empty;

  /// <summary>
  /// Seo Filename 
  /// </summary>
  public string? SeoFilename { get; set; }

  /// <summary>
  /// Alt Attribute
  /// </summary>
  public string? AltAttribute { get; set; }

  /// <summary>
  /// Title Attribute
  /// </summary>
  public string? TitleAttribute { get; set; }

  /// <summary>
  /// Phone
  /// </summary>
  public bool IsNew { get; set; }

  /// <summary>
  /// Virtual Path
  /// </summary>
  public string VirtualPath { get; set; } = string.Empty;

  /// <summary>
  /// MediaBinary_Id
  /// </summary>
  public long MediaBinary_Id { get; set; }

  /// <summary>
  /// Name
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Size 
  /// </summary>
  public long Size { get; set; }

  /// <summary>
  /// Size MediaType 
  /// /// </summary>
  public int MediaType { get; set; }

  public DateTime CreateDate { get; set; }

  /// <summary>
  /// Show
  /// </summary>
  public bool IsShow { get; set; }

  public int CreateUid { get; set; }
  #endregion

}
