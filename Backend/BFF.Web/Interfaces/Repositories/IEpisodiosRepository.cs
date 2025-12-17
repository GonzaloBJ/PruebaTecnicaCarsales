using BFF.Web.Core.Pagination;
using BFF.Web.DTOs;
using BFF.Web.Filters;

namespace BFF.Web.Interfaces.Repositories
{
    public interface IEpisodiosRepository
    {
        Task<ResultPagination<EpisodioDto>> GetEpisodiosAsync(EpisodiosFilter filter);
    }
}