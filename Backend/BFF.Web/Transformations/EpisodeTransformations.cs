using BFF.Web.Models;
using BFF.Web.Core.Pagination;
using BFF.Web.DTOs;
using System.Text.Json;
using BFF.Web.Interfaces.Integrations;

namespace BFF.Web.Transformations
{
    public class EpisodeTransformations : ITranformations<EpisodioDto>
    {
        private readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public ResultPagination<EpisodioDto> TransformSingle(string json)
        {
            Episode episode = JsonSerializer.Deserialize<Episode>(json, jsonOptions)
                ?? throw new InvalidOperationException("Episodio nulo");

            return new ResultPagination<EpisodioDto>(
                pageIndex: 1,
                pageSize: 1,
                count: 1,
                data: [new EpisodioDto
                {
                    Id = episode.id,
                    Nombre = episode.name,
                    EmitidoEn = episode.air_date,
                    TemporadaConNumeroEpisodio = episode.episode
                }]
            );
        }

        public ResultPagination<EpisodioDto> TransformPagination(string json, int pageIndex)
        {
            Pagination pagination = JsonSerializer.Deserialize<Pagination>(json, jsonOptions)
                ?? throw new InvalidOperationException("Paginación nula");

            IEnumerable<Episode?> episodios = pagination.results.Select(e =>
                JsonSerializer.Deserialize<Episode>(e.ToString() ?? "", jsonOptions));

            if (episodios == null || !episodios.Any())
                return ResultPagination<EpisodioDto>.Empty();

            IEnumerable<EpisodioDto> episodiosDto = episodios.Select(e => new EpisodioDto
            {
                Id = e!.id,
                Nombre = e.name,
                EmitidoEn = e.air_date,
                TemporadaConNumeroEpisodio = e.episode
            });

            return new ResultPagination<EpisodioDto>(
                pageIndex: pageIndex,
                pageSize: episodiosDto.Count(),
                count: pagination!.info.count,
                data: [.. episodiosDto]
            );
        }
    }
}
