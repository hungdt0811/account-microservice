using Account.Microservice.Core.Helpers;

namespace Account.Microservice.Web.ApiModels.Medias;

public class MediaModel
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the picture mime type
  /// </summary>
  public string MimeType { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the SEO friendly filename of the picture
  /// </summary>
  public string SeoFilename { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the "alt" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
  /// </summary>
  public string AltAttribute { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the "title" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
  /// </summary>
  public string TitleAttribute { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets a value indicating whether the picture is new
  /// </summary>
  public bool IsNew { get; set; }


  /// <summary>
  /// Gets or sets the picture virtual path
  /// </summary>
  public string VirtualPath { get; set; } = string.Empty;


  public int CreatedUserId { get; set; }

  public string CreatedUserName { get; set; } = string.Empty;

  public DateTime CreatedDate { get; set; }

  public int Size { get; set; }

  public string MediaUrl { get; internal set; } = string.Empty;

  public bool IsPiucture
  {
    get
    {
      return PictureHelper.GetFileTypeFromMimeType(MimeType) == 1;
    }
  }

  public bool IsMedia
  {
    get
    {
      return PictureHelper.GetFileTypeFromMimeType(MimeType) == 2;
    }
  }

  public bool IsFile
  {
    get
    {
      return PictureHelper.GetFileTypeFromMimeType(MimeType) == 0;
    }
  }
}
