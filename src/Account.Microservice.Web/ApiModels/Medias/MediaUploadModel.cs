namespace Account.Microservice.Web.ApiModels.Medias;

public class MediaUploadModel
{
  public int CreateId { get; set; }
  public int MediaType { get; set; }
  /// <summary>
  /// Gets or sets the "alt" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
  /// </summary>
  public string? AltAttribute { get; set; }

  /// <summary>
  /// Gets or sets the "title" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
  /// </summary>
  public string? TitleAttribute { get; set; }
  public IFormFile? file { get; set; }
}
