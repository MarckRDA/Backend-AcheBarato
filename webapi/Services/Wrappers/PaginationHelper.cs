using System;
using System.Collections.Generic;
using Domain.Common;
using webapi.Services.URIBuilder;

namespace webapi.Services.Wrappers
{
    public class PaginationHelper
    {
        public static PageResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, QueryParameters validFilter, int totalRecords, IURIService uriService, string route)
        {
            var respose = new PageResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new QueryParameters(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new QueryParameters(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new QueryParameters(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new QueryParameters(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}