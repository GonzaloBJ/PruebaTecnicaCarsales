using BFF.Web.Filters;
using BFF.Web.Core.API;
using BFF.Web.Core.Pagination;

namespace BFF.Web.Interfaces
{
    public interface IItemInfoService<T> where T : class
    {
        public Task<ServiceResult<ResultPagination<T>>> GetItemsInfoAsync(EpisodiosFilter filter);
    }
}