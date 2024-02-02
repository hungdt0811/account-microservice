using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Data;

namespace Account.Microservice.Core.Services.Paginations;
public class PaginationService : IPaginationService
{
  public PaginationInfo GetPaginationInfo<T>(IPagedList<T> pagedList)
  {
    return new PaginationInfo
    {
      PageCurent = pagedList.PageIndex + 1,
      TotalPages = pagedList.TotalPages,
      PageSize = pagedList.PageSize,
      TotalItem = pagedList.TotalCount,
      HasPreviousPage = pagedList.HasPreviousPage,
      HasNextPage = pagedList.HasNextPage
    };
  }
}
