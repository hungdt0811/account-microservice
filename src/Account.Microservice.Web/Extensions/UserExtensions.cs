using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Web.ApiModels.Users;
using Account.Microservice.Web.Helpers;

namespace Account.Microservice.Web.Extensions;

public static class UserExtensions
{
  public static UserModel ToModel(this User user)
  {
    var model = user.Map<UserModel>();
    
    //model!.IsAdmin = user.IsAdmin();
    //model!.SellerName = user.GetSellerName();
    return model!;
  }
  public static UserModel ToModel(this User user, bool isRemember)
  {
    var model = new UserModel
    {
      RememberToken = user.RememberToken,
      //Code = user.Code,
      Email = user.Email,
      //FirstName = user.FirstName,
      //LastName = user.LastName!,
      Id = user.Id,
      IsRememberMe = isRemember,
      //RoleId = user.RoleId,
      //Phone = user.Phone!,
      //IsAdmin = user.IsAdmin(),
      //SellerName = user.GetSellerName(),
      //MediaId = user.MediaId
    };
    return model!;
  }
  public static UserResponse ToResponseModel(this User user)
  {
    var model = user.Map<UserResponse>();
    return model!;
  }

  public static UserStudentResponse ToResponseUserStudentModel(this User user)
  {
    var model = user.Map<UserStudentResponse>();
    return model!;
  }

  public static IPagedList<UserStudentResponse> ToPagedListResponse(this IPagedList<User> pagedList)
  {
    var userResponses = pagedList.Select(user => user.ToResponseUserStudentModel()).ToList();
    return new PagedList<UserStudentResponse>(userResponses, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalCount);
  }
}
