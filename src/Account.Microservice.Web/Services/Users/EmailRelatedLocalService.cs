using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.Services.Medias;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Services.Medias;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Account.Microservice.Web.Services.Users;

public class EmailRelatedLocalService : IEmailRelatedLocalService
{
  private readonly ILogger<EmailRelated> _logger;
  private readonly IEmailRelatedService _emailRelatedService;
  private readonly IUserService _userService;
  private readonly IMediaLocalService _mediaLocalService;
  private readonly IMediaService _mediaService;

  public EmailRelatedLocalService(ILogger<EmailRelated> logger, IEmailRelatedService emailRelatedService, IUserService userService, IMediaLocalService mediaLocalService, IMediaService mediaService)
  {
    _logger = logger;
    _emailRelatedService = emailRelatedService;
    _userService = userService;
    _mediaLocalService = mediaLocalService;
    _mediaService = mediaService;
  }

  public Task<BaseResponseModel> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<BaseResponseModel> GetAllAsync(int userId)
  {
    throw new NotImplementedException();
  }
}
