using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.MediaAggregate;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Helpers;
using Account.Microservice.Core.Services.Medias;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.Extensions;
using NuGet.Packaging.Signing;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Account.Microservice.Web.ApiModels.Medias;
using Account.Microservice.Core.Services.Users;
using User = Account.Microservice.Core.Entities.UserAggregate.User;

namespace Account.Microservice.Web.Services.Medias;

public class MediaLocalService : IMediaLocalService
{
  private readonly IMediaService _mediaService;
  private readonly MediaSettings _mediaSettings;
  private readonly ILogger<Media> _logger;
  private readonly AwsS3Settings _awsS3Settings;
  private readonly IUserService _userService;
  public MediaLocalService(IMediaService mediaService,
    MediaSettings mediaSettings,
    ILogger<Media> logger,
    AwsS3Settings awsS3Settings,
    IUserService userService)
  {
    _mediaService = mediaService;
    _mediaSettings = mediaSettings;
    _logger = logger;
    _awsS3Settings = awsS3Settings;
    _userService = userService;
  }


  public async Task<BaseResponseModel> GetById(int id)
  {
    try
    {
      var media = await _mediaService.GetById(id);
      return ResponseModel.RespondSuccess(media.ToResponseModel(_awsS3Settings));
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error get media_id_{id} {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError, ex.Message);
    }
  }

  public async Task<BaseResponseModel> GetList(int page, int count, int userId, bool selectOnlyImage = false, int? sellerId = null)
  {
    try
    {

      if (sellerId != null && sellerId > 0)
      {
        var user = await _userService.GetUserBySellerId(sellerId.Value);
        if (user == null)
        {
          return ResponseModel.RespondFailure(ErrorCode.InvalidInput);
        }

        var list = await _mediaService.GetListByUserId(page, count, user.Id, selectOnlyImage, userId);
        return ResponseModel.RespondSuccess(list.Select(m => m.ToResponseModel(_awsS3Settings)).ToList(), list.TotalCount);
      }
      else
      {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
          return ResponseModel.RespondFailure(ErrorCode.InvalidInput);
        }
       
        var list = await _mediaService.GetListByUserId(page, count, user.Id, selectOnlyImage);
        return ResponseModel.RespondSuccess(list.Select(m => m.ToResponseModel(_awsS3Settings)).ToList(), list.TotalCount);
      }

    }
    catch (Exception ex)
    {
      _logger.LogError($"Error get list media {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError, ex.Message);
    }
  }

  public async Task<BaseResponseModel> ReadS3Async(string key)
  {
    //var c = _awsS3Service.GetMediaIdAsync(key);

    //var stream = await _awsS3Service.GetBytesAsync(key);
    var user = await _userService.GetUserByIdAsync(1);//fake data
    return new BaseResponseModel()
    {
      Data = null,
      ErrorCode = ErrorCode.Success,
      Success = true,
    };
  }

  public async Task<BaseResponseModel> UploadAsync(MediaUploadModel mediaUploadModel)
  {
    try
    {
      var file = mediaUploadModel.file;
      if (file == null)
      {
        return ResponseModel.RespondFailure(ErrorCode.InvalidInput);
      }
      var mediaModels = new List<MediaResponseModel>();

      //and it's name
      var fileName = file.FileName;
      //stream to read the bytes
      var stream = file.OpenReadStream();
      var pictureBytes = new byte[stream.Length];
      stream.Read(pictureBytes, 0, pictureBytes.Length);

      //file extension and it's type
      var fileExtension = Path.GetExtension(fileName);
      if (!string.IsNullOrEmpty(fileExtension))
        fileExtension = fileExtension.ToLowerInvariant();

      var contentType = file.ContentType;

      if (string.IsNullOrEmpty(contentType))
      {
        contentType = PictureHelper.GetContentType(fileExtension);
      }
      var seoFileName = Path.GetFileNameWithoutExtension(fileName);

      var media = await _mediaService.UploadMedia(pictureBytes, contentType, seoFileName, mediaUploadModel.CreateId, file.Length, mediaUploadModel.AltAttribute!, mediaUploadModel.TitleAttribute!, mediaUploadModel.MediaType, true);

      mediaModels.Add(media.ToResponseModel(_mediaSettings));
      return ResponseModel.RespondSuccess(mediaModels);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error upload media {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError, ex.Message);
    }
  }

  public async Task<BaseResponseModel> UploadS3Async(MediaUploadModel mediaUploadModel)
  {
    try
    {
      var file = mediaUploadModel.file;
      if (file == null)
      {
        return ResponseModel.RespondFailure(ErrorCode.InvalidInput);
      }
      // var mediaModels = new List<MediaResponseModel>();

      //and it's name
      var fileName = file.FileName;
      //stream to read the bytes
      var stream = file.OpenReadStream();
      var pictureBytes = new byte[stream.Length];
      stream.Read(pictureBytes, 0, pictureBytes.Length);

      //file extension and it's type
      var fileExtension = Path.GetExtension(fileName);
      if (!string.IsNullOrEmpty(fileExtension))
        fileExtension = fileExtension.ToLowerInvariant();

      var contentType = file.ContentType;

      if (string.IsNullOrEmpty(contentType))
      {
        contentType = PictureHelper.GetContentType(fileExtension);
      }
      var seoFileName = Path.GetFileNameWithoutExtension(fileName);
      string keyName = $"SellerId_{mediaUploadModel.CreateId}/{Guid.NewGuid().ToString()}{fileExtension}";
      var media = await _mediaService.UploadMedia(pictureBytes, contentType, seoFileName, mediaUploadModel.CreateId, file.Length, mediaUploadModel.AltAttribute!, mediaUploadModel.TitleAttribute!, keyName);
      // upload to s3
      //_awsS3Service.UploadBytesToS3(pictureBytes, keyName);
      //string url = $"https://{_awsS3Settings.BucketName}.s3.{_awsS3Settings.BucketRegion}.amazonaws.com/{keyName}";
      //await _mediaService.UpdateUrl(media!.Id, url);

      // mediaModels.Add(media!.ToResponseModel(_awsS3Settings));
      return ResponseModel.RespondSuccess(media!.ToResponseModel(_awsS3Settings));
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error upload media to s3 {ex.Message} - {ex.StackTrace!}");
      return ResponseModel.RespondFailure(ErrorCode.SystemError, ex.Message);
    }
  }

  public async Task<BaseResponseModel> DeleteAsync(int id)
  {
    try
    {
      var report = await _mediaService.GetById(id);
      if (report == null)
      {
        return ResponseModel.RespondFailure(ErrorCode.NoData);
      }
      await _mediaService.HideMedia(id);

      return new BaseResponseModel()
      {
        Data = { },
        ErrorCode = ErrorCode.Success,
        Success = true,
        Messages = "データが削除されました。"
      };
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error delete report id {id} - {ex.Message}", ex);
      return ResponseModel.RespondFailure(ErrorCode.SystemError, ex.Message);
    }
  }

}
