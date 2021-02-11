using System;
using Domain.Common;

namespace webapi.Services.URIBuilder
{
    public interface IURIService
    {
        Uri GetPageUri(QueryParameters filter, string route);
    }

}