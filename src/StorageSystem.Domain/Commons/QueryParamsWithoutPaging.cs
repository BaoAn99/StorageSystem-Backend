using StorageSystem.Domain.Commons.Interfaces;

namespace StorageSystem.Domain.Commons
{
    public class QueryParamsWithoutPaging
    {
        public FilterQuery[] Filters { get; set; }
        public SortQuery Sort { get; set; }
        //public bool? IsDeleted { get; set; }
        //public bool? IsPublished { get; set; }
        //public virtual IQueryable<T> ApplyFilter<T>(IQueryable<T> query) where T : IEntity
        //{
        //    query = (IsDeleted.HasValue &&  !IsPublished.HasValue) ? query.Where(x => x.Equals(IsDeleted.Value)) : query;
        //    return query;
        //}
    }
}
