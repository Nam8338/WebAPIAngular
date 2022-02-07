using System;
using System.Collections.Generic;
using Web_API.Filters;
using Web_API.Models;
using Web_API.Services;

namespace Web_API.Responses
{
    public class PageResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }


        public PageResponse(List<StudentDTO> data ,int pageNumber, int pageSize) : base (data)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Message = null;
            this.Succeeded = true;
        }

        public static PageResponse<List<StudentDTO>> CreatePagedReponse(List<StudentDTO> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PageResponse<List<StudentDTO>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            if(validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages)
            {
                var nextPage = (int)validFilter.PageNumber + 1;
                respose.NextPage = nextPage;
            }    
            if (validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages)
            {
                var previousPage = (int)validFilter.PageNumber - 1;
                respose.PreviousPage = previousPage;
            }    
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
