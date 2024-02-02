using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Services.Paginations;
public class PaginationInfo
{
  public int PageCurent { get; set; }
  public int TotalPages { get; set; }
  public int PageSize { get; set; }
  public int TotalItem { get; set; }
  public bool HasPreviousPage { get; set; }
  public bool HasNextPage { get; set; }
}
