namespace StorageSystem.Domain.Commons
{
    public class QueryParamsWithoutPaging
    {
        public FilterQuery[] Filter { get; set; }
        public SortQuery Sort { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPublished { get; set; }
    }
}
