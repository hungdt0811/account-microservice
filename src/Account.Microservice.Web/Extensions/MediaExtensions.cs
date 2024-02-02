using System.Security.Cryptography.X509Certificates;
using Account.Microservice.Core.Entities.MediaAggregate;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Services.Medias;
using Account.Microservice.Web.ApiModels.Medias;

namespace Account.Microservice.Web.Extensions;

public static class MediaExtensions
{
  public static MediaResponseModel ToResponseModel(this Media media, MediaSettings mediaSettings)
  {
    if (media == null)
      return new MediaResponseModel();
    var model = new MediaResponseModel()
    {
      Id = media.Id,
      Name = media.Name,
      SeoFilename = media.SeoFilename!,
      TitleAttribute = media.TitleAttribute!,
      AltAttribute = media.AltAttribute!,
      MimeType = media.MimeType,
      VirtualPath = media.VirtualPath,
      MediaUrl = GetUrlMedia(mediaSettings, media.VirtualPath),
      CreatedUserId = media.CreateUid,
      CreatedDate = media.CreateDate
    };

    return model;
  }
  private static string GetUrlMedia(MediaSettings mediaSettings, string virtualPath)
  {
    return $"{mediaSettings.UrlCdn}{virtualPath.Replace("~", "")}";
  }
  public static MediaResponseModel ToResponseModel(this Media media, AwsS3Settings awsS3Settings)
  {
    if (media == null)
      return new MediaResponseModel();
    string url = $"https://{awsS3Settings.BucketName}.s3.{awsS3Settings.BucketRegion}.amazonaws.com/";
    var model = new MediaResponseModel()
    {
      Id = media.Id,
      Name = media.Name,
      SeoFilename = media.SeoFilename!,
      TitleAttribute = media.TitleAttribute!,
      AltAttribute = media.AltAttribute!,
      MimeType = media.MimeType,
      VirtualPath = media.VirtualPath,
      MediaUrl = $"{url}{media.VirtualPath}",
      CreatedUserId = media.CreateUid,
      CreatedDate = media.CreateDate
    };

    return model;
  }
}
