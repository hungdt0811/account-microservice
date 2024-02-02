using System.Diagnostics;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Web.ApiModels;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.Microservice.Web.Api;

[ApiController]
public class LoginController : Controller
{
  private readonly IUserService _userService;
  private readonly IUserLocalService _userLocalService;

  public LoginController(IUserService userService, IUserLocalService userLocalService)
  {
    _userService = userService; 
    _userLocalService = userLocalService;
  }

  [HttpPost]
  [Route("api/v1/login/cms")]
  public async Task<IActionResult> CreateUser(LoginModel loginModel)
  {
    var result = await _userLocalService.LoginUserCms(loginModel);
    if (!result.Success)
    {
      return Ok(new BaseResponseModel
      {
        Success = false,
        ErrorMessages = result.ErrorMessages
      });
    }

    return Ok(new BaseResponseModel
    {
      Success = true,
      Data = result.Data,
      Token = result.Token
    });
  }
}
