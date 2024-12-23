namespace CandidateHub.Application.Common.Models
{
    public class PageResponse<TResult> where TResult : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public List<TResult> Result { get; set; }
    }
}
