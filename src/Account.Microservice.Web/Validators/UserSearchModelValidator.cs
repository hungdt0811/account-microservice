using Account.Microservice.Web.ApiModels.Users;
using FluentValidation;

namespace Account.Microservice.Web.Validators;

public class UserSearchModelValidator: AbstractValidator<UserSearchModel>
{
  public UserSearchModelValidator()
  {
    //RuleFor(p => p.Email).NotEmpty()
    //    .When(x => !string.IsNullOrEmpty(x.Email));
  }
}
