using System.Net;
using BFF.Web.Core.API;
using BFF.Web.Core.Pagination;
using BFF.Web.DTOs;
using BFF.Web.Filters;
using BFF.Web.Interfaces.Integrations;
using BFF.Web.Interfaces.Repositories;
using BFF.Web.Transformations;

namespace BFF.Web.Repositories
{
    public class EpisodiosAPIRepository : IEpisodiosRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IQueryBuilder<EpisodiosFilter> _queryBuilder;
        private readonly ITranformations<EpisodioDto> _episodeTransformations;

        public EpisodiosAPIRepository(HttpClient httpClient, IQueryBuilder<EpisodiosFilter> queryBuilder, ITranformations<EpisodioDto> episodeTransformations)
        {
            _httpClient = httpClient;
            _queryBuilder = queryBuilder;
            _episodeTransformations = episodeTransformations;
        }

        public async Task<ResultPagination<EpisodioDto>> GetEpisodiosAsync(EpisodiosFilter filter)
        {
            string query = _queryBuilder.Build(filter);
            HttpResponseMessage? response = await _httpClient.GetAsync(query);

            // En caso de no encontrar resultados no arroje error
            if (response.StatusCode == HttpStatusCode.NotFound)
                return ResultPagination<EpisodioDto>.Empty();

            // Valida que el resultado sea correcto 
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return filter.Id.HasValue
                ? _episodeTransformations.TransformSingle(json)
                : _episodeTransformations.TransformPagination(json, filter.PageIndex);
        }

    }
}