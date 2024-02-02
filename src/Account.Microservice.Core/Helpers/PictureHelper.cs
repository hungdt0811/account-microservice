using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Services.Medias;

namespace Account.Microservice.Core.Helpers;
public static class PictureHelper
{

  //contentType is not always available 
  //that's why we manually update it here
  //http://www.sfsu.edu/training/mimetype.htm
  public static string GetContentType(string fileExtension)
  {
    switch (fileExtension)
    {
      case ".bmp":
        return "image/bmp";
      case ".gif":
        return "image/gif";
      case ".jpeg":
      case ".jpg":
      case ".jpe":
      case ".jfif":
      case ".pjpeg":
      case ".pjp":
        return "image/jpeg";
      case ".png":
        return "image/png";
      case ".tiff":
      case ".tif":
        return "image/tiff";
      default:
        return string.Empty;
    }
  }

  public static int GetFileTypeFromMimeType(string mimeType)
  {
    if (mimeType == null)
      return 0;
    var parts = mimeType.Split('/');
    var firstPart = parts[0];
    switch (firstPart)
    {
      case "image":
        return 1;
      case "video":
        return 2;
    }
    return 0;
  }

  ///// <summary>
  ///// Gets image format from the content type
  ///// </summary>
  ///// <param name="contentType"></param>
  ///// <returns></returns>
  //public static ImageFormat GetImageFormatFromContentType(string contentType)
  //{
  //    switch (contentType)
  //    {
  //        case "image/bmp":
  //            return ImageFormat.Bmp;;
  //        case "image/gif":
  //            return ImageFormat.Gif;
  //        case "image/jpeg":
  //            return ImageFormat.Jpeg;
  //        case "image/png":
  //            return ImageFormat.Png;
  //        case "image/tiff":
  //            return ImageFormat.Tiff;
  //        case "image/x-icon":
  //            return ImageFormat.Icon;
  //    }
  //    //default jpeg
  //    return ImageFormat.Jpeg;;
  //}

  /// <summary>
  /// Parses pictureSize string and returns a <see cref="PictureSize"/> instance if a valid pictureSize is passed 
  /// We expect pictureSize as one of WidthxHeight|Width*Height|WidthXHeight|Width,Height
  /// </summary>
  /// <param name="pictureSize"></param>
  /// <param name="pictureName"></param>
  /// <returns></returns>
  public static PictureSize? ParsePictureSize(string pictureSize, string pictureName)
  {
    if (string.IsNullOrEmpty(pictureSize))
      return null;

    //we expect picture sizes as WidthxHeight, Width*Height, WidthXHeight, Width,Height
    var sizeParts = pictureSize.Split('x', 'X', '*', ',');
    if (sizeParts.Length != 2)
      return null;

    int width, height;

    try
    {
      width = int.Parse(sizeParts[0]);
      height = int.Parse(sizeParts[1]);
    }
    catch (Exception)
    {
      return null;
    }

    return new PictureSize()
    {
      Name = pictureName,
      Width = width,
      Height = height
    };
  }



}
