using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Constants;

namespace Account.Microservice.Core.Helpers;
public class BaseResponse
{
  public bool Success { get; set; }
  public string? Messages { get; set; }
  public dynamic? Data { get; set; }
}
public static class DataResponse
{
  public static BaseResponse RespondSuccess(string message = "")
  {
    return new BaseResponse
    {
      Success = true,
      Messages = message
    };
  }
  public static BaseResponse RespondSuccess(dynamic additionalData)
  {
    return new BaseResponse
    {
      Success = true,
      Data = additionalData
    };
  }

  public static BaseResponse RespondFailure(string message = "")
  {
    return new BaseResponse
    {
      Success = false,
      Messages = message
    };
  }
}
