using System;
using Web_API.Filters;

namespace Web_API.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
   
    }
}
