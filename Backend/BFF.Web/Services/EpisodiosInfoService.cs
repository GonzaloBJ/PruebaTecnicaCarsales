using System.Net;
using System.Text.Json;
using BFF.Web.Filters;
using BFF.Web.Interfaces;
using BFF.Web.Models;
using BFF.Web.Core.API;
using BFF.Web.Core.Pagination;
using BFF.Web.DTOs;
using BFF.Web.Transformations;
using BFF.Web.Enumerators;

namespace BFF.Web.Services
{
    public class EpisodiosInfoService: IItemInfoService<EpisodioDto>
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string ENDPOINT;

        public EpisodiosInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            ENDPOINT = EQueryItem.episode.ToString();
        }

        public async Task<ServiceResult<ResultPagination<EpisodioDto>>> GetItemsInfoAsync(EpisodiosFilter filter)
        {
            try
            {
                string queryString = ENDPOINT;

                // Agrega los parámetros de consulta según el filtro proporcionado
                if (filter.PageIndex > 1)
                    queryString += $"?page={filter.PageIndex}";

                if (filter.PageIndex == 1 && filter.Id.HasValue)
                    queryString += $"/{filter.Id.Value}";

                // Realiza la solicitud GET de forma asíncrona
                HttpResponseMessage? response = await _httpClient.GetAsync(queryString);

                // En caso de no encontrar resultados no arroje error
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return ServiceResult<ResultPagination<EpisodioDto>>.Fail("No encontrado.", (int)HttpStatusCode.NotFound);

                // Valida que el resultado sea correcto 
                response.EnsureSuccessStatusCode();

                string? json = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON a una paginacion
                if (filter.Id.HasValue)
                {
                    Episode? episodio = JsonSerializer.Deserialize<Episode>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (episodio == null)
                        return ServiceResult<ResultPagination<EpisodioDto>>.Fail("No encontrado.", (int)HttpStatusCode.NotFound);

                    // Transforma el resultado extraido del servicio a un DTO para el frontend
                    return ServiceResult<ResultPagination<EpisodioDto>>.Ok(EpisodeTransformations.TransformSingleEpisodeToResultPagination(episodio));
                }
                else
                {
                    Pagination? episodiosPagination = JsonSerializer.Deserialize<Pagination>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (episodiosPagination == null)
                        return ServiceResult<ResultPagination<EpisodioDto>>.Fail("No encontrado.", (int)HttpStatusCode.NotFound);

                    // Transforma el resultado extraido del servicio a un DTO para el frontend
                    return ServiceResult<ResultPagination<EpisodioDto>>.Ok(EpisodeTransformations.TransformEpisodePaginationToResultPagination(episodiosPagination!, filter.PageIndex));
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<ResultPagination<EpisodioDto>>.Fail($"Error al obtener los episodios: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
