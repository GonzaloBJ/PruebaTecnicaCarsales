using System.Net;
using BFF.Web.Filters;
using BFF.Web.Interfaces.Services;
using BFF.Web.Core.API;
using BFF.Web.Core.Pagination;
using BFF.Web.DTOs;
using BFF.Web.Interfaces.Integrations;
using BFF.Web.Repositories;
using BFF.Web.Interfaces.Repositories;

namespace BFF.Web.Services
{
    public class EpisodiosInfoService : IItemInfoService<EpisodioDto>
    {
        private readonly IQueryBuilder<EpisodiosFilter> _queryBuilder;
        private readonly IEpisodiosRepository _episodiosAPIRepository;

        public EpisodiosInfoService( IQueryBuilder<EpisodiosFilter> queryBuilder, IEpisodiosRepository episodiosAPIRepository)
        {
            _queryBuilder = queryBuilder;
            _episodiosAPIRepository = episodiosAPIRepository;
        }

        public async Task<ServiceResult<ResultPagination<EpisodioDto>>> GetItemsInfoAsync(EpisodiosFilter filter)
        {
            try
            {
                string query = _queryBuilder.Build(filter);

                var result = await _episodiosAPIRepository.GetEpisodiosAsync(filter);
                
                if (!result.Data.Any())
                    return ServiceResult<ResultPagination<EpisodioDto>>
                        .Fail("No encontrado.", (int)HttpStatusCode.NotFound);

                return ServiceResult<ResultPagination<EpisodioDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<ResultPagination<EpisodioDto>>
                    .Fail($"Error al obtener los episodios: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
