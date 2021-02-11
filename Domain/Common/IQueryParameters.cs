namespace Domain.Common
{
    public interface IQueryParameters
    {
        string Search { get; set; }
        int PageNumber { get; set; }
        string OrderBy { get; set; }
        int PageSize { get; set; }

    }
}