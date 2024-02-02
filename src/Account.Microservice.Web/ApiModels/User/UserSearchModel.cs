namespace Account.Microservice.Web.ApiModels.Users;

public class UserSearchModel
{
  public string CompanyName { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public int RoleId { get; set; }
  public DateTime? From { get; set; }
  public DateTime? To { get; set; }
  //paging
  public int Count { get; set; }
  public int Page { get; set; }
  public int CurrentPage { get; set; } = 1;
  public int PageSize { get; set; } = 10;

  public string SortBy { get; set; } = string.Empty;

  public bool Ascending { get; set; }

  public int? Status { get; set; }
}
