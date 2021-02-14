using System;
using Domain.Common;
using Microsoft.AspNetCore.WebUtilities;

namespace webapi.Services.URIBuilder
{
    public class URIService : IURIService
    {
        private readonly string _baseUri;
        public URIService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(QueryParameters filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.Limit.ToString());
            return new Uri(modifiedUri);
        }
    }
}

