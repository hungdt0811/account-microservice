using Account.Microservice.Core.Constants;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.Authorization;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Account.Microservice.Web.Api;

[Route("api/v1/[controller]")]
[ApiController]
public abstract class BaseApiController : Controller
{
  private readonly JsonSerializerSettings _serializerSettings;
  protected BaseApiController()
  {
    _serializerSettings = new JsonSerializerSettings
    {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      ContractResolver = new CamelCasePropertyNamesContractResolver(),
      PreserveReferencesHandling = PreserveReferencesHandling.None,
    };
  }

  [ApiExplorerSettings(IgnoreApi = true)]
  private string? GetHeader(string key)
  {
    var value = string.Empty;
    if (Request.Headers.TryGetValue(key, out var headerValue))
    {
      value = headerValue;
    }
    return value;
  }

  [ApiExplorerSettings(IgnoreApi = true)]
  public string GetToken()
  {
    return GetHeader(AppConstants.TokenHeader)!;
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public int UserId(IJwtUtils jwtUtils)
  {
    var id = jwtUtils.ValidateToken(GetToken().Replace("Bearer", "").Trim());
    return id ?? 0;
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public new IActionResult Response(dynamic obj)
  {
    return Json(obj, _serializerSettings);
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult Respond(string message)
  {
    return Ok(message);
  }

  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult RespondSuccess()
  {
    return RespondSuccess();
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult RespondSuccess(dynamic additionalData)
  {
    return Response(new BaseResponseModel()
    {
      Success = true,
      Data = additionalData
    });
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult RespondSuccess(dynamic additionalData, int total)
  {
    return Response(new BaseResponseModel()
    {
      Success = true,
      Data = additionalData,
      Total = total
    });
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult RespondSuccess(string successMessage, string contextName, dynamic additionalData)
  {
    return RespondSuccess(additionalData);
  }

  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult RespondFailure()
  {
    return RespondFailure();
  }
  [ApiExplorerSettings(IgnoreApi = true)]
  public IActionResult RespondFailure(ErrorCode code)
  {
    return Response(new BaseResponseModel()
    {
      Success = true,
      Data = null,
      ErrorCode = code
    });
  }
}
