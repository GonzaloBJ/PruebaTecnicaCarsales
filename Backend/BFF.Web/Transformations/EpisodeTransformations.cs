using BFF.Web.Models;
using BFF.Web.Core.Pagination;
using BFF.Web.DTOs;
using System.Text.Json;

namespace BFF.Web.Transformations
{
    public static class EpisodeTransformations
    {
        public static ResultPagination<EpisodioDto> TransformEpisodePaginationToResultPagination(Pagination episodePagination, int pageIndex = 1)
        {
            // Deserializar el resultado a una lista de episodios
            IEnumerable<Episode?>? episodios = episodePagination?.results?.Select(e =>
            JsonSerializer.Deserialize<Episode>(e.ToString() ?? "", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }));

            if (episodios == null && episodios?.Count() > 0)
                return new ResultPagination<EpisodioDto>(0, 0, 0, new List<EpisodioDto>());

            IEnumerable<EpisodioDto> episodiosDto = episodios.Select(e => new EpisodioDto
            {
                Id = e.id,
                Nombre = e.name,
                EmitidoEn = e.air_date,
                TemporadaConNumeroEpisodio = e.episode
            });

            ResultPagination<EpisodioDto> resultPagination = new ResultPagination<EpisodioDto>(
                pageIndex: pageIndex,
                pageSize: episodiosDto.Count(),
                count: episodePagination!.info.count,
                data: episodiosDto.ToList()
            );

            return resultPagination;
        }

        public static ResultPagination<EpisodioDto> TransformSingleEpisodeToResultPagination(Episode episode)
        {
            EpisodioDto episodioDto = new EpisodioDto
            {
                Id = episode.id,
                Nombre = episode.name,
                EmitidoEn = episode.air_date,
                TemporadaConNumeroEpisodio = episode.episode
            };
            return new ResultPagination<EpisodioDto>(
                pageIndex: 1,
                pageSize: 1,
                count: 1,
                data: new List<EpisodioDto> { episodioDto }
            );
        }
    }
}
