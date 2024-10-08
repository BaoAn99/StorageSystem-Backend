namespace StorageSystem.Domain.Commons
{
    public class QueryParams : QueryParamsWithoutPaging
    {
        public PageQuery Page { get; set; }
        public QueryParams()
        {
        }

        public QueryParams(int pageNumber, int pageSize)
        {
            Page = new PageQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
