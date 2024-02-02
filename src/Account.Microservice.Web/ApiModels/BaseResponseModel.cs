using Account.Microservice.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Account.Microservice.Web.ApiModels;

public class BaseResponseModel
{
  public bool Success { get; set; }

  public string? ErrorMessages { get; set; }

  public ErrorCode ErrorCode { get; set; }

  public string? Messages { get; set; }

  public dynamic? Data { get; set; }

  public int Total { get; set; }

  public int Status { get; set; }

  public string? Token { get; set;}
}
public static class ResponseModel
{
  public static BaseResponseModel RespondSuccess(string message = "")
  {
    return new BaseResponseModel
    {
      Success = true,
      Messages = message
    };
  }
  public static BaseResponseModel RespondSuccess(dynamic additionalData)
  {
    return new BaseResponseModel
    {
      Success = true,
      Data = additionalData
    };
  }

  public static BaseResponseModel RespondSuccess(dynamic additionalData, int total)
  {
    return new BaseResponseModel
    {
      Success = true,
      Data = additionalData,
      Total = total
    };
  }

  public static BaseResponseModel RespondFailure(ErrorCode code)
  {
    return new BaseResponseModel
    {
      Success = false,
      Data = null,
      ErrorCode = code,
      Status = (int)code
    };
  }
  public static BaseResponseModel RespondFailure(ErrorCode code, string? message)
  {
    return new BaseResponseModel
    {
      Success = false,
      Data = null,
      ErrorCode = code,
      ErrorMessages = message
    };
  }
}
