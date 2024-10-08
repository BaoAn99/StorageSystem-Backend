namespace StorageSystem.Domain.Commons
{
    public class PageQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PageQuery()
        {
        }

        public PageQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
