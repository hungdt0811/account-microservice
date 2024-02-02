using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Data;

namespace Account.Microservice.Core.Services.Paginations;
public interface IPaginationService
{
  PaginationInfo GetPaginationInfo<T>(IPagedList<T> pagedList);
}
