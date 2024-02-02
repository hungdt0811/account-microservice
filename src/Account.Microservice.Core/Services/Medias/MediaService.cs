using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Autofac.Features.Metadata;
using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.MediaAggregate;
using Account.Microservice.Core.Entities.MediaAggregate.Specification;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Helpers;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.Medias;
public class MediaService : IMediaService
{
  private readonly IRepository<Media> _mediaRepository;
  private readonly MediaSettings _mediaSettings;
  private readonly IAppFileProvider _fileProvider;
  public MediaService(IRepository<Media> mediaRepository, MediaSettings mediaSettings, IAppFileProvider fileProvider)
  {
    _mediaRepository = mediaRepository;
    _mediaSettings = mediaSettings;
    _fileProvider = fileProvider;
  }
  public async Task<Media> UploadMedia(
            byte[] pictureBinary,
            string mimeType,
            string seoFilename,
            int createdUserId,
            long contentLength,
            string altAttribute,
            string titleAttribute,
            int mediaType,
            bool validateBinary = true)
  {
    mimeType = CommonHelper.EnsureNotNull(mimeType);
    mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

    seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

    //if (validateBinary)
    //  pictureBinary = ValidateMedia(pictureBinary, mimeType);

    //#pragma warning disable CS8601 // Possible null reference assignment.
    var picture = new Media
    {
      MimeType = mimeType,
      SeoFilename = seoFilename,
      Name = seoFilename,
      AltAttribute = altAttribute,
      TitleAttribute = titleAttribute,
      MediaType = mediaType,
      CreateDate = DateTime.UtcNow,
      CreateUid = createdUserId,
      Size = contentLength,
      IsShow = true
    };

    await _mediaRepository.AddAsync(picture);

    var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
    var fileName = $"{picture.Id:0000000}_0.{lastPart}";
    var virtualPath = GetPictureVirtualPath(fileName);
    picture.VirtualPath = virtualPath;

    await _mediaRepository.UpdateAsync(picture);

    //if (StoreInDb)
    //  UpdateMediaBinary(picture, pictureBinary);

    SaveMediaInFile(picture.Id, pictureBinary, mimeType);

    return picture;
  }

  public async Task<Media?> UploadMedia(
            byte[] pictureBinary,
            string mimeType,
            string seoFilename,
            int createdUserId,
            long contentLength,
            string altAttribute,
            string titleAttribute,
            string keyName)
  {
    mimeType = CommonHelper.EnsureNotNull(mimeType);
    mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

    seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);
    var picture = new Media
    {
      MimeType = mimeType,
      SeoFilename = seoFilename,
      Name = seoFilename,
      AltAttribute = altAttribute,
      TitleAttribute = titleAttribute,
      MediaType = GetFileTypeFromMimeType(mimeType),
      CreateDate = DateTime.UtcNow,
      CreateUid = createdUserId,
      Size = contentLength,
      IsShow = true
    };
    await _mediaRepository.AddAsync(picture);
    picture.VirtualPath = keyName;
    await _mediaRepository.UpdateAsync(picture);
    return picture;
  }


  protected virtual void SaveMediaInFile(long pictureId, byte[] pictureBinary, string mimeType)
  {
    var lastPart = GetFileExtensionFromMimeType(mimeType);
    var fileName = $"{pictureId:0000000}_0.{lastPart}";
    _fileProvider.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
  }
  protected virtual string GetPictureLocalPath(string fileName)
  {
    if (string.IsNullOrEmpty(fileName))
    {
      return string.Empty;
    }
    string path = _mediaSettings.PictureSavePath;// CommonHelper.MapPath(_mediaSettings.PictureSavePath);
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
    return Path.Combine(path, fileName);
  }
  public virtual string GetFileExtensionFromMimeType(string mimeType)
  {
    if (mimeType == null)
      return "";

    //TODO use FileExtensionContentTypeProvider to get file extension

    var parts = mimeType.Split('/');
    var lastPart = parts[parts.Length - 1];
    switch (lastPart)
    {
      case "pjpeg":
        lastPart = "jpg";
        break;
      case "x-png":
        lastPart = "png";
        break;
      case "x-icon":
        lastPart = "ico";
        break;
    }

    return lastPart;
  }

  public virtual int GetFileTypeFromMimeType(string mimeType)
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

  protected virtual string GetPictureVirtualPath(string fileName)
  {
    return Path.Combine((_mediaSettings.PicturePath), fileName).Replace("\\", "/");
  }

  public async Task<bool> UpdateUrl(int mediaId, string url)
  {
    var media = await _mediaRepository.GetByIdAsync(mediaId);
    if (media != null)
    {
      media.VirtualPath = url;
      await _mediaRepository.UpdateAsync(media);
      return true;
    }
    return false;
  }

  public async Task<IPagedList<Media>> GetListByUserId(int page, int count, int? userId = null, bool selectOnlyImage = false, int? userAdminId = 0)
  {
    var specification = new MediaFillterByUserIdSpecification(count * (page - 1), count, userId, selectOnlyImage, userAdminId);
    var itemsOnPage = await _mediaRepository.ListAsync(specification);

    var filterBy = new MediaFillterByUserIdSpecification(userId, selectOnlyImage, userAdminId);
    var totalItems = await _mediaRepository.CountAsync(filterBy);
    if (itemsOnPage != null && itemsOnPage.Count > 0 && totalItems > 0)
    {
      return new PagedList<Media>(itemsOnPage, page, count, totalItems);
    }
    return new PagedList<Media>(new List<Media>(), 0, 0);
  }

  public async Task<Media> GetById(int id)
  {
    var specification = new MediaSpecification(id);
    var media = await _mediaRepository.FirstOrDefaultAsync(specification);
    return media!;
  }

  public async Task HideMedia(int id)
  {
    var specification = new MediaSpecification(id);
    var report = await _mediaRepository.FirstOrDefaultAsync(specification);
    if (report != null)
    {
      report.IsShow = false;
    }
    await _mediaRepository.UpdateAsync(report!);
  }
}
