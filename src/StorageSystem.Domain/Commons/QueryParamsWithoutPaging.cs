namespace StorageSystem.Domain.Commons
{
    public class QueryParamsWithoutPaging
    {
        public FilterQuery[] Filters { get; set; }
        public SortQuery Sort { get; set; }
        //public bool? IsDeleted { get; set; }
        //public bool? IsPublished { get; set; }
    }
}
