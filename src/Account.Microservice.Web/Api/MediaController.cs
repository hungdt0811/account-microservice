using Account.Microservice.Core.Helpers;
using Account.Microservice.Core.Services.Medias;
using Microsoft.AspNetCore.Mvc;
using Account.Microservice.Web.Extensions;
using Account.Microservice.Core.Entities.MediaAggregate;
using Account.Microservice.Core.Constants;
using Account.Microservice.Web.Services.Medias;
using Account.Microservice.Web.Authorization;
using System.Net.Mime;
using Account.Microservice.Web.ApiModels.Medias;

namespace Account.Microservice.Web.Api;
public class MediaController : BaseApiController
{
  //private readonly IMediaService _mediaService;
  //private readonly MediaSettings _mediaSettings;
  private readonly IMediaLocalService _mediaLocalService;
  private readonly IJwtUtils _jwtUtils;
  public MediaController(IMediaLocalService mediaLocalService, IJwtUtils jwtUtils)
  {
    _mediaLocalService = mediaLocalService;
    _jwtUtils = jwtUtils;
  }

  [HttpPost("upload")]
  public async Task<IActionResult> Upload([FromForm] MediaUploadModel mediaUploadModel)
  {
    var model = await _mediaLocalService.UploadAsync(mediaUploadModel);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest, model);
    }
    return Ok(model);
  }
  [Authorize]
  [HttpPost("upload-s3")]
  public async Task<IActionResult> UploadS3([FromForm] MediaUploadModel mediaUploadModel)
  {
    int userId = UserId(_jwtUtils);
    if (userId == 0)
    {
      return StatusCode(StatusCodes.Status401Unauthorized, ModelState);
    }
    mediaUploadModel.CreateId = userId;
    var model = await _mediaLocalService.UploadS3Async(mediaUploadModel);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest, model);
    }
    return Ok(model);
  }
  [HttpGet("ProfilePic")]
  public async Task<IActionResult> GetProfilePicture()
  {
    var re = await _mediaLocalService.ReadS3Async("0000435_0.jpeg");
    byte[] imageStream = re.Data!;

    return File(imageStream, "image/jpeg");
  }
  [Authorize]
  [HttpGet("list")]
  public async Task<IActionResult> GetList(int page, int count, bool selectOnlyImage = false, int? sellerId = null)
  {
    int userId = UserId(_jwtUtils);
    if (userId == 0)
    {
      return StatusCode(StatusCodes.Status401Unauthorized, ModelState);
    }

    var model = await _mediaLocalService.GetList(page, count, userId, selectOnlyImage, sellerId);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest);
    }
    return Ok(model);
  }
  [HttpGet("detail")]
  public async Task<IActionResult> GetById(int id)
  {
    var model = await _mediaLocalService.GetById(id);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest, model);
    }
    return Ok(model);
  }

  [Authorize]
  [HttpDelete("delete")]
  public async Task<IActionResult> Delete(int id)
  {
    var model = await _mediaLocalService.DeleteAsync(id);
    if (!model.Success)
    {
      return StatusCode(StatusCodes.Status400BadRequest, model);
    }
    return Ok(model);
  }

  [Authorize]
  [HttpPost("uploadpictures")]
  public async Task<IActionResult> UploadPictures(List<IFormFile> files)
  {
    long size = files.Sum(f => f.Length);

    foreach (var formFile in files)
    {
      if (formFile.Length > 0)
      {
        var filePath = Path.GetTempFileName();

        using (var stream = System.IO.File.Create(filePath))
        {
          await formFile.CopyToAsync(stream);
        }
      }
    }

    // Process uploaded files
    // Don't rely on or trust the FileName property without validation.

    return Ok(new { count = files.Count, size });
  }
}
